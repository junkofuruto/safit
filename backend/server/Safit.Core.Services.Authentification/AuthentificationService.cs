using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Safit.Core.Services.Authentification;

public class AuthentificationService : IAuthentificationService
{
    private static readonly TimeSpan tokenLifespan = TimeSpan.FromHours(1);
    private IConfiguration _configuration;

    public AuthentificationService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<AuthentificationData> ExtractAsync(AuthentificationToken token, CancellationToken ct = default)
    {
        return await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = Encoding.UTF8.GetBytes(_configuration["Safit:Bearer:IssuerSigningKey"]!);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Safit:Bearer:ValidIssuer"],
                ValidAudience = _configuration["Safit:Bearer:ValidAudience"],
                ValidAlgorithms = new[] { "HS256" },
                IssuerSigningKey = new SymmetricSecurityKey(signingKey)
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token.Value.Substring(7), validationParameters, out var validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                var id = Convert.ToInt64(jwtToken?.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
                return new AuthentificationData() { Id = id };
            }
            catch
            {
                throw new UnauthorizedAccessException($"Unable to extract data from token");
            }
        }, ct);
    }

    public async Task<AuthentificationToken> GenerateAsync(AuthentificationData auth, CancellationToken ct = default)
    {
        return await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = _configuration["Safit:Bearer:ValidIssuer"],
                Audience = _configuration["Safit:Bearer:ValidAudience"],
                Expires = DateTime.UtcNow.Add(tokenLifespan),
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", auth.Id.ToString())
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Safit:Bearer:IssuerSigningKey"]!)),
                    SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthentificationToken() { Value = tokenHandler.WriteToken(token) };
        }, ct);
    }
}