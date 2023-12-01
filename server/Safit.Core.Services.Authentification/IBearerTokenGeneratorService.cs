using Safit.Core.Domain.Entities;

namespace Safit.Core.Services.Authentification;

public interface IBearerTokenGeneratorService
{
    public Task<string> GenerateAsync(User user);
}