namespace Safit.Core.Domain.Service;

public interface IBearerTokenDispatcherService
{
    public Task ExtractUserAsync(string tokenString);
}