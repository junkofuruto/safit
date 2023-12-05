namespace Safit.Core.Services.Authorisation;

public interface IAuthorisationService
{
    public Task LoginAsync(string username, string password, CancellationToken cancellationToken);
    public Task RegisterAsync(string username, string password, CancellationToken cancellationToken);
}