using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface IAuthorizationService
{
    public Task<AuthentificationToken> LoginAsync(string username, string password, CancellationToken ct = default);
    public Task<AuthentificationToken> RegisterAsync(
        string email, string username, string password, 
        string firstName, string lastName, string? profileSrc, 
        CancellationToken ct = default);
}