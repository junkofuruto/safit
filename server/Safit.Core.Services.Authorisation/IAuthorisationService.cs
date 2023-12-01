using Safit.Core.Domain.Entities;

namespace Safit.Core.Services.Authorisation;

public interface IAuthorisationService
{
    public Task<User> LoginAsync(string username, string password, CancellationToken cancellationToken);
    public Task<User> RegisterAsync(string username, string password, CancellationToken cancellationToken);
}