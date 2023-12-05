using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Safit.Core.Services.Authorisation;

public sealed class AuthorisationService : IAuthorisationService
{
    private IConfiguration configuration;

    public AuthorisationService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task LoginAsync(string username, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var user = await context.Users.Where(x => x.Username == username && x.PasswordHash == EncryptPassword(password)).FirstOrDefaultAsync(cancellationToken);
        //if (user is null) throw new UserOrPasswordInvalidExeption();
        //return user;
    }

    public async Task RegisterAsync(string username, string password, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        //var user = new User()
        //{
        //    Username = username,
        //    PasswordHash = EncryptPassword(password)
        //};
        //await context.Users.AddAsync(user, cancellationToken);
        //try { await context.SaveChangesAsync(); }
        //catch { throw new UserAlreadyRegisteredException(); }
        //return user;
    }

    private string EncryptPassword(string password)
    {
        using var encryptor = SHA256.Create();
        var bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{configuration["Safit:Authorisation:Salt"]}{password}")).Select(x => x.ToString("x2"));
        return string.Join("", bytes);
    }
}