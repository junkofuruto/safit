using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

namespace Safit.Core.Domain.Service;

public interface IAuthentificationService
{
    public Task<AuthentificationToken> GenerateAsync(AuthentificationData auth, CancellationToken ct = default);
    public Task<AuthentificationData> ExtractAsync(AuthentificationToken token, CancellationToken ct = default);
}