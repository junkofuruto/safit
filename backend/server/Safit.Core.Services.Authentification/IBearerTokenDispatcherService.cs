using Safit.Core.Domain.Entities;

namespace Safit.Core.Services.Authentification;

public interface IBearerTokenDispatcherService
{
    public Task<User> ExtractUserAsync(string tokenString);
}