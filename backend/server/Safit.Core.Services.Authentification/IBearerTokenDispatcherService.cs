namespace Safit.Core.Services.Authentification;

public interface IBearerTokenDispatcherService
{
    public Task ExtractUserAsync(string tokenString);
}