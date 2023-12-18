using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Services.Content;

public class PostService : IPostService
{
    private IProfileService profileService;
    private ITrainerService trainerService;
    private IRepositoryWrapper repositoryWrapper;
    private IRecommendationService recommendationService;
    private IAuthentificationService authentificationService;

    public PostService(
        IProfileService profileService,
        ITrainerService trainerService,
        IRepositoryWrapper repositoryWrapper, 
        IRecommendationService recommendationService,
        IAuthentificationService authentificationService)
    {
        this.profileService = profileService;
        this.trainerService = trainerService;
        this.repositoryWrapper = repositoryWrapper;
        this.recommendationService = recommendationService;
        this.authentificationService = authentificationService;
    }

    public async Task<Post> CreateAsync(AuthentificationToken token, long sportId, long? courseId, string content, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        if (await trainerService.IsSpecialized(token, sportId) == false) throw new ArgumentException("trainer is not specialized");
        var post = new Post()
        {
            TrainerId = userInfo.Id,
            SportId = sportId,
            CourseId = courseId,
            Content = content
        };
        await repositoryWrapper.Post.Create(post);
        await repositoryWrapper.SaveChangesAsync();
        return post;
    }

    public async Task<Post> GetById(AuthentificationToken token, long postId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var post = (await repositoryWrapper.Post.FindByCondition(x => x.Id == postId)).First();
        if (post.Course is not null && post.Course.CourseAccesses.Any(x => x.UserId == userInfo.Id) == false)
            throw new AccessViolationException("no access");
        await recommendationService.ViewPostAsync(token, postId, ct);
        return post;
    }

    public async Task<Post> GetRecommendedAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        Post? post = null;
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        do
        {
            var recommendedTag = await recommendationService.GetRecommendedTag(token, ct);
            if (recommendedTag == null) continue;
            var posts = (await repositoryWrapper.Post.FindByCondition(
                x => x.Tags.Select(
                    y => y.Name).Contains(recommendedTag.Name))).Where(
                x => x.Course == null).OrderBy(x => Ulid.NewUlid()).First();
        } 
        while (post == null);
        await recommendationService.ViewPostAsync(token, post.Id, ct);
        return post;
    }
}
