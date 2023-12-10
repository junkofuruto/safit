namespace Safit.Core.Domain.Service;

public interface IBearerTokenGeneratorService
{
    public Task<string> GenerateAsync();
}