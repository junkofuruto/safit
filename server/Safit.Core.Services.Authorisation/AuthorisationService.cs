using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Safit.Core.DataAccess;
using Safit.Core.Domain.Entities;
using Safit.Core.Services.Authorisation.Exceptions;
using System.Security.Cryptography;
using System.Text;

namespace Safit.Core.Services.Authorisation;

public sealed class AuthorisationService : IAuthorisationService
{
    private DatabaseContext context;
    private IConfiguration configuration;

    public AuthorisationService(DatabaseContext context, IConfiguration configuration)
    {
        this.configuration = configuration;
        this.context = context;
    }

    public async Task<User> LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = await context.Users.Where(x => x.Username == username && x.PasswordHash == EncryptPassword(password)).FirstOrDefaultAsync(cancellationToken);
        if (user is null) throw new UserOrPasswordInvalidExeption();
        return user;
    }

    public async Task<User> RegisterAsync(string username, string password, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            Username = username,
            PasswordHash = EncryptPassword(password)
        };
        await context.Users.AddAsync(user, cancellationToken);
        try { await context.SaveChangesAsync(); }
        catch { throw new UserAlreadyRegisteredException(); }
        return user;
    }

    private string EncryptPassword(string password)
    {
        using var encryptor = SHA256.Create();
        var bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{configuration["Safit:Authorisation:Salt"]}{password}")).Select(x => x.ToString("x2"));
        return string.Join("", bytes);
    }
}