using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Safit.Core.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Safit.Core.Services.Authentification.Token;

public sealed class BearerTokenGeneratorService : IBearerTokenGeneratorService
{
    private static readonly TimeSpan tokenLifespan = TimeSpan.FromHours(1);
    private IConfiguration configuration;

    public BearerTokenGeneratorService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<string> GenerateAsync(User user)
    {
        return await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = configuration["Safit:Bearer:ValidIssuer"],
                Audience = configuration["Safit:Bearer:ValidAudience"],
                Expires = DateTime.UtcNow.Add(tokenLifespan),
                Subject = new ClaimsIdentity(new[] 
                { 
                    new Claim("id", user.Id.ToString()), 
                    new Claim("usernaname", user.Username!.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Safit:Bearer:IssuerSigningKey"]!)),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        });
    }
}