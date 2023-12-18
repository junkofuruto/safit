using Microsoft.EntityFrameworkCore;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Services.Content;

public class RecommendationService : IRecommendationService
{
    private static readonly float viewFactor = 0.001f;
    private static readonly float likeFactor = 0.01f;
    private static readonly float commentFactor = 0.02f;

    private IRepositoryWrapper repositoryWrapper;
    private IAuthentificationService authentificationService;

    public RecommendationService(
        IRepositoryWrapper repositoryWrapper,
        IAuthentificationService authentificationService)
    {
        this.repositoryWrapper = repositoryWrapper;
        this.authentificationService = authentificationService;
    }
    public async Task<Tag> GetRecommendedTag(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token);
        var recommendations = await repositoryWrapper.Recommendation.FindByCondition(x => x.UserId == userInfo.Id);
        if (recommendations.Any() == false) return (await repositoryWrapper.Tag.FindAll()).OrderBy(x => Ulid.NewUlid()).First();
        return await recommendations.OrderBy(x => x.Weight).Take(3).OrderBy(x => Ulid.NewUlid()).Select(x => x.Tag).FirstAsync();
    }

    public async Task CommentVideoAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var video = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId, ct);
        if (video == null) throw new ArgumentException("invalid video id");
        var tags = video.First().Tags.ToArray();
        await Task.WhenAll(tags.Select(x => UpdateRecommendationTag(userInfo.Id, x.Id, commentFactor, ct)));
    }
    
    public async Task LikeVideoAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var video = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId, ct);
        if (video == null) throw new ArgumentException("invalid video id");
        var tags = video.First().Tags.ToArray();
        await Task.WhenAll(tags.Select(x => UpdateRecommendationTag(userInfo.Id, x.Id, likeFactor, ct)));
    }

    public async Task ViewPostAsync(AuthentificationToken token, long postId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var post = await repositoryWrapper.Post.FindByCondition(x => x.Id == postId, ct);
        if (post == null) throw new ArgumentException("invalid postt id");
        var tags = post.First().Tags.ToArray();
        await Task.WhenAll(tags.Select(x => UpdateRecommendationTag(userInfo.Id, x.Id, viewFactor, ct)));
    }

    public async Task ViewVideoAsync(AuthentificationToken token, long videoId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var video = await repositoryWrapper.Video.FindByCondition(x => x.Id == videoId, ct);
        if (video == null) throw new ArgumentException("invalid video id");
        var tags = video.First().Tags.ToArray();
        await Task.WhenAll(tags.Select(x => UpdateRecommendationTag(userInfo.Id, x.Id, viewFactor, ct)));
    }

    private async Task<Recommendation> UpdateRecommendationTag(long userId, long tagId, float factor, CancellationToken ct)
    {
        Recommendation? recommendation = null;
        var recs = await repositoryWrapper.Recommendation.FindByCondition(x => x.UserId == userId && x.TagId == tagId);
        if (recs.Any())
        {
            recommendation = await recs.FirstAsync();
            recommendation.Weight = Convert.ToSingle(Math.Log2(recommendation.Weight)) * factor;
            await repositoryWrapper.Recommendation.Update(recommendation, ct);
        }
        else
        {
            recommendation = new Recommendation() { UserId = userId, TagId = tagId, Weight = 2f };
            await repositoryWrapper.Recommendation.Create(recommendation, ct);
        }
        await repositoryWrapper.SaveChangesAsync();
        return recommendation;
    }
}
