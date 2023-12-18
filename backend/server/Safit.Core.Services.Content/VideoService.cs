using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;
using System.Runtime.CompilerServices;

namespace Safit.Core.Services.Content;

public class VideoService : IVideoService
{
    private IAuthentificationService authentificationService;
    private IRecommendationService recommendationService;
    private IRepositoryWrapper repositoryWrapper;
    private IProfileService profileService;
    private ITrainerService trainerService;
    private IFileService fileService;

    public VideoService(
        IAuthentificationService authentificationService, 
        IRecommendationService recommendationService,
        IRepositoryWrapper repositoryWrapper,
        IProfileService profileService,
        ITrainerService trainerService,
        IFileService fileService)
    {
        this.authentificationService = authentificationService;
        this.recommendationService = recommendationService;
        this.repositoryWrapper = repositoryWrapper;
        this.profileService = profileService;
        this.trainerService = trainerService;
        this.fileService = fileService;
    }

    public async Task<Video> CreateAsync(AuthentificationToken token, string[] tags, long sportId, long? courseId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        if (await trainerService.IsSpecialized(token, sportId, ct) == false) throw new ArgumentException("trainer is not specialized");
        var video = new Video()
        {
            TrainerId = userInfo.Id,
            SportId = sportId,
            CourseId = courseId,
            Views = 0,
            Visible = false
        };
        var newTags = tags.Select(x => new Tag() { Name = x });
        await Task.WhenAll(newTags.Select(x => repositoryWrapper.Tag.Create(x, ct)));
        await repositoryWrapper.SaveChangesAsync();
        await repositoryWrapper.Video.Create(video);
        foreach (var tag in newTags) video.Tags.Add(tag);
        await repositoryWrapper.SaveChangesAsync();
        return video;
    }

    public async Task<Comment> CreateCommentAsync(AuthentificationToken token, long videoId, long? branchId, string text, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var comment = new Comment() { UserId = userInfo.Id, VideoId = videoId, BranchId = branchId, Value = text };
        await repositoryWrapper.Comment.Create(comment, ct);
        await repositoryWrapper.SaveChangesAsync();
        return comment;
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Comment>> GetCommentsAsync(AuthentificationToken token, long videoId, int count, int offset, long? originId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var videos = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId && x.Visible, ct);
        if (videos.Any() == false) throw new ArgumentException("no video with such id");
        var video = await videos.FirstAsync();
        if (video.Course != null)
        {
            var courseAccess = await repositoryWrapper.CourseAccess.FindByCondition(x => x.CourseId == video.CourseId && x.UserId == userInfo.Id, ct);
            if (courseAccess.Any() == false)
                throw new ArgumentException("not owner of a course");
        }
        var comments = await repositoryWrapper.Comment.FindByCondition(x => x.VideoId == videoId && x.BranchId == originId, ct);
        return comments.Skip(offset).Take(count).ToList();
    }

    public async Task<ReadOnlyMemory<byte>> GetContentAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var videos = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId && x.Visible, ct);
        if (videos.Any() == false) throw new ArgumentException("no video with such id");
        var video = await videos.FirstAsync();
        if (video.Course != null)
        {
            var courseAccess = await repositoryWrapper.CourseAccess.FindByCondition(x => x.CourseId == video.CourseId && x.UserId == userInfo.Id, ct);
            if (courseAccess.Any() == false) throw new ArgumentException("not owner of a course");
        }
        var sources = await repositoryWrapper.FetchSource.FindByCondition(x => x.VideoId == videoId);
        var data = await Task.WhenAll(sources.Select(x => fileService.GetAsync(x.Source, ct)));
        var result = new byte[0];
        data.Select(x => result.Concat(x.ToArray()));
        await recommendationService.ViewVideoAsync(token, videoId);
        return result;
    }

    public async Task<Video> GetInfoByIdAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var videos = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId && x.Visible, ct);
        if (videos.Any() == false) throw new ArgumentException("no video with such id");
        var video = await videos.FirstAsync();
        if (video.Course != null)
        {
            var courseAccess = await repositoryWrapper.CourseAccess.FindByCondition(x => x.CourseId == video.CourseId && x.UserId == userInfo.Id, ct);
            if (courseAccess.Any() == false)
                throw new ArgumentException("not owner of a course");
        }
        return video;
    }

    public async Task<int> GetLikesAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var videos = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId && x.Visible, ct);
        if (videos.Any() == false) throw new ArgumentException("no video with such id");
        var video = await videos.FirstAsync();
        if (video.Course != null)
        {
            var courseAccess = await repositoryWrapper.CourseAccess.FindByCondition(x => x.CourseId == video.CourseId && x.UserId == userInfo.Id, ct);
            if (courseAccess.Any() == false)
                throw new ArgumentException("not owner of a course");
        }
        return video.Users.Count;
    }

    public async Task<Video> GetRecommendedAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var recommendedTag = await recommendationService.GetRecommendedTag(token, ct);
        if (recommendedTag == null) throw new ArgumentException("no tag found");
        var videos = await repositoryWrapper.Video.FindByCondition(
            x => x.CourseId == null && x.Visible && x.Tags.Select(
                x => x.Name).Contains(recommendedTag.Name), ct);
        if (videos.Any() == false) throw new ArgumentException("no videos found");
        return videos.First();
    }

    public async Task<int> ToggleLikeAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var videos = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId && x.Visible, ct);
        if (videos.Any() == false) throw new ArgumentException("no video with such id");
        var user = await repositoryWrapper.User.FindByCondition(x => x.Id == userInfo.Id);
        var video = await videos.FirstAsync();
        if (video.Users.Select(x => x.Id).Contains(userInfo.Id)) video.Users.Remove(user.First());
        else video.Users.Add(user.First());
        await repositoryWrapper.SaveChangesAsync();
        return (await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId)).First().Users.Count();
    }

    public async Task<Video> UploadAsync(AuthentificationToken token, long videoId, ReadOnlyMemory<byte> data, CancellationToken ct = default)
    {
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        var userInfo = await authentificationService.ExtractAsync(token);
        var videos = await repositoryWrapper.Video.FindByCondition(x =>
            x.Id == videoId && x.TrainerId == userInfo.Id && x.Visible == false);
        if (videos.Any() == false) throw new ArgumentException("trainer is not a owner of a video");
        var fileInfo = await fileService.UploadFileAsync(data, "mp4", ct);
        var video = await videos.FirstAsync();
        video.FetchSources.Add(new FetchSource() { Source = fileInfo.Name, VideoId = videoId });
        await repositoryWrapper.SaveChangesAsync();
        return video;
    }
}
