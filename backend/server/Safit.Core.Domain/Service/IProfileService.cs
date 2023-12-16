using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

namespace Safit.Core.Domain.Service;

public interface IProfileService
{
    public Task<User> GetFullInfoAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<User> GetInfoAsync(string username, CancellationToken ct = default);
    public Task<User> UpdateDataAsync(AuthentificationToken token, ProfileModificationData modificationData, CancellationToken ct = default);
    public Task<User> PromoteAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<IQueryable<Sport>> GetSpecialisationsAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<IQueryable<Sport>> GetSpecialisationsAsync(string username, CancellationToken ct = default);
    public Task<Sport> AddSpecialisationAsync(AuthentificationToken token, CancellationToken ct = default);
    public Task<Sport> CreateSportAsync(AuthentificationToken token, Sport sport, CancellationToken ct = default);
    public Task<bool> IsTrainerAsync(AuthentificationToken token, CancellationToken ct = default);
}