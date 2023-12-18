using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service;
using System.Security.Cryptography;
using System.Text;
using System.Security.Authentication;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

namespace Safit.Core.Services.Authorization;

public sealed class AuthorizationService : IAuthorizationService
{
    private IAuthentificationService authentificationService;
    private IRepositoryWrapper repository;
    private IConfiguration configuration;

    public AuthorizationService(
        IAuthentificationService authentificationService,
        IRepositoryWrapper repository,
        IConfiguration configuration)
    {
        this.authentificationService = authentificationService;
        this.repository = repository;
        this.configuration = configuration;
    }

    public string EncryptPassword(string password)
    {
        using var encryptor = SHA256.Create();
        var bytes = encryptor.ComputeHash(Encoding.UTF8.GetBytes($"{configuration["Safit:Authorisation:Salt"]}{password}"));
        return string.Join("", bytes.Select(x => (char)x));
    }

    public string GenerateAccessToken()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, 100).Select(s => s[Random.Shared.Next(s.Length)]).ToArray());
    }

    public async Task<AuthentificationToken> LoginAsync(string username, string password, CancellationToken ct = default)
    {
        password = EncryptPassword(password);
        var selection = await repository.User.FindByCondition(x => x.Username == username && x.Password == password, ct);
        var result = await selection.FirstOrDefaultAsync(ct);
        if (result == null) throw new InvalidCredentialException($"incorrect username or password");
        var authData = new AuthentificationData() { Id = result.Id };
        return await authentificationService.GenerateAsync(authData);
    }

    public async Task<AuthentificationToken> RegisterAsync(
        string email, string username, string password, 
        string firstName, string lastName, string? profileSrc, 
        CancellationToken ct = default)
    {
        var user = new User()
        {
            Email = email,
            Username = username,
            Password = EncryptPassword(password),
            FirstName = firstName,
            LastName = lastName,
            ProfileSrc = profileSrc,
            Token = GenerateAccessToken()
        };
        await repository.User.Create(user, ct);
        await repository.SaveChangesAsync();
        var authData = new AuthentificationData() { Id = user.Id };
        return await authentificationService.GenerateAsync(authData);
    }
}