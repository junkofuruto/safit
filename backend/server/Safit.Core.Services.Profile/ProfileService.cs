using Microsoft.Extensions.Configuration;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Safit.Core.Services.Profile;

public class ProfileService : IProfileService
{
    private IAuthentificationService authentificationService;
    private IRepositoryWrapper repositoryWrapper;
    private IConfiguration configuration;

    public ProfileService(
        IAuthentificationService authentificationService, 
        IRepositoryWrapper repositoryWrapper,
        IConfiguration configuration)
    {
        this.authentificationService = authentificationService;
        this.repositoryWrapper = repositoryWrapper;
        this.configuration = configuration;
    }

    public async Task<Sport> AddSpecialisationAsync(AuthentificationToken token, long sportId, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if ((await repositoryWrapper.Trainer.FindByCondition(x => x.Id == userInfo.Id)).Any() == false) 
            throw new ArgumentException("user is not a trainer");
        var specialisation = new Specialisation() { TrainerId = userInfo.Id, SportId = sportId };
        await repositoryWrapper.Specialisation.Create(specialisation, ct);
        await repositoryWrapper.SaveChangesAsync();
        return specialisation.Sport;
    }

    public async Task<Sport> CreateSportAsync(AuthentificationToken token, Sport sport, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if ((await repositoryWrapper.Trainer.FindByCondition(x => x.Id == userInfo.Id)).Any() == false)
            throw new ArgumentException("user is not a trainer");
        await repositoryWrapper.Sport.Create(sport, ct);
        await repositoryWrapper.SaveChangesAsync();
        return sport;
    }

    public async Task<User> GetFullInfoAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var users = await repositoryWrapper.User.FindByCondition(x => x.Id == userInfo.Id);
        return users.First();
    }

    public async Task<User> GetInfoAsync(string username, CancellationToken ct = default)
    {
        var users = await repositoryWrapper.User.FindByCondition(x => x.Username == username);
        if (users.Any() == false) throw new ArgumentException("no user with such username");
        var selectedUser = users.First();
        var user = new User();
        user.Username = selectedUser.Username;
        user.FirstName = selectedUser.FirstName;
        user.LastName = selectedUser.LastName;
        user.ProfileSrc = selectedUser.ProfileSrc;
        user.Trainer = selectedUser.Trainer;
        return user;
    }

    public async Task<IQueryable<Sport>> GetSpecialisationsAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        if ((await repositoryWrapper.Trainer.FindByCondition(x => x.Id == userInfo.Id)).Any() == false)
            throw new ArgumentException("user is not a trainer");
        return (await repositoryWrapper.Specialisation.FindByCondition(x => x.TrainerId == userInfo.Id)).Select(x => x.Sport);
    }

    public async Task<IQueryable<Sport>> GetSpecialisationsAsync(string username, CancellationToken ct = default)
    {
        var user = (await repositoryWrapper.User.FindByCondition(x => x.Username == username)).First();
        if ((await repositoryWrapper.Trainer.FindByCondition(x => x.Id == user.Id)).Any() == false)
            throw new ArgumentException("user is not a trainer");
        return (await repositoryWrapper.Specialisation.FindByCondition(x => x.TrainerId == user.Id)).Select(x => x.Sport);
    }

    public async Task<bool> IsTrainerAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        return (await repositoryWrapper.Trainer.FindByCondition(x => x.Id == userInfo.Id)).Any();
    }

    public async Task<User> PromoteAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var user = (await repositoryWrapper.User.FindByCondition(x => x.Id == userInfo.Id, ct)).First();
        var trainer = new Trainer() { Id = user.Id };
        await repositoryWrapper.Trainer.Create(trainer, ct);
        await repositoryWrapper.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateDataAsync(AuthentificationToken token, ProfileModificationData modificationData, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var user = (await repositoryWrapper.User.FindByCondition(x => x.Id == userInfo.Id, ct)).First();
        user.Password = modificationData.Password is null ? user.Password : EncryptPassword(modificationData.Password);
        user.FirstName = modificationData.FirstName is null ? user.FirstName : modificationData.FirstName;
        user.LastName = modificationData.LastName is null ? user.LastName : modificationData.LastName;
        user.ProfileSrc = modificationData.ProfileSrc;
        await repositoryWrapper.User.Update(user, ct);
        await repositoryWrapper.SaveChangesAsync();
        return user;
    }

    public string EncryptPassword(string password)
    {
        using var encryptor = SHA256.Create();
        var bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{configuration["Safit:Authorisation:Salt"]}{password}"));
        return string.Join("", bytes.Select(x => (char)x));
    }
}
