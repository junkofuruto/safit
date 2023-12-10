using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service;
using Safit.Core.Services.Authorisation.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Safit.Core.Services.Authorisation;

public sealed class AuthorisationService : IAuthorisationService
{
    private IRepositoryWrapper repository;
    private IConfiguration configuration;
    private ILogger logger;

    public AuthorisationService(
        IRepositoryWrapper repository,
        IConfiguration configuration,
        ILogger logger)
    {
        this.repository = repository;
        this.configuration = configuration;
        this.logger = logger;
    }

    public async Task<User> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var passwordHash = EncryptPassword(password);
        var userSet = await repository.User.FindByCondition(x => x.Username == username && x.Password == passwordHash, cancellationToken);
        var user = await userSet.FirstOrDefaultAsync(cancellationToken);

        if (user is null) throw new InvalidCredentialsExeption();
        else return user;
    }

    public async Task<User> RegisterAsync(string username, string password, string email, string firstName, string lastName, string? profileSource, CancellationToken cancellationToken)
    {
        var passwordHash = EncryptPassword(password);
        var user = new User() { Username = username, Password = passwordHash, Email = email, FirstName = firstName, LastName = lastName, ProfileSrc = profileSource };

        await repository.User.Create();
    }

    private string EncryptPassword(string password)
    {
        using var encryptor = SHA256.Create();
        var bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{configuration["Safit:Authorisation:Salt"]}{password}")).Select(x => x.ToString("x2"));
        return string.Join("", bytes);
    }
}