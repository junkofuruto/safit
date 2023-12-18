using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Services.Profile;

public class TrainerService : ITrainerService
{
    private IAuthentificationService authentificationService;
    private IRepositoryWrapper repositoryWrapper;
    private IProfileService profileService;

    public TrainerService(
        IAuthentificationService authentificationService, 
        IRepositoryWrapper repositoryWrapper,
        IProfileService profileService)
    {
        this.authentificationService = authentificationService;
        this.repositoryWrapper = repositoryWrapper;
        this.profileService = profileService;
    }

    public async Task<Sport> CreateSport(AuthentificationToken token, string name, string preview, string description, CancellationToken ct = default)
    {
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        var sport = new Sport()
        {
            Name = name,
            Description = description,
            PreviewSrc = preview
        };
        await repositoryWrapper.Sport.Create(sport, ct);
        await repositoryWrapper.SaveChangesAsync();
        return sport;
    }

    public async Task<IQueryable<Sport>> FindSportByName(string name, CancellationToken ct = default)
    {
        return await repositoryWrapper.Sport.FindByCondition(x => x.Name.Contains(name), ct);
    }

    public async Task<IQueryable<Course>> GetCourses(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        return await repositoryWrapper.Course.FindByCondition(x => x.TrainerId == userInfo.Id, ct);
    }

    public async Task<IQueryable<Post>> GetPosts(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        return await repositoryWrapper.Post.FindByCondition(x => x.TrainerId == userInfo.Id, ct);
    }

    public async Task<IQueryable<Product>> GetProducts(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        return await repositoryWrapper.Product.FindByCondition(x => x.TrainerId == userInfo.Id, ct);
    }

    public async Task<IQueryable<Video>> GetVideos(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        return await repositoryWrapper.Video.FindByCondition(x => x.TrainerId == userInfo.Id, ct);
    }

    public async Task<bool> IsSpecialized(AuthentificationToken token, long sport, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if (await profileService.IsTrainerAsync(token, ct) == false) throw new ArgumentException("user is not a trainer");
        return (await repositoryWrapper.Specialisation.FindByCondition(x => x.TrainerId == userInfo.Id && x.SportId == sport)).Any();
    }
}
