using Safit.Core.Domain.Model;

namespace Safit.Core.Domain.Service;

public interface IAuthorisationService
{
    public Task<User> LoginAsync(string username, string password, CancellationToken cancellationToken);
    public Task<User> RegisterAsync(string username, string password, string email, string firstName, string lastName, string? profilePicture, CancellationToken cancellationToken);
}