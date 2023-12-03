using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Safit.Core.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Safit.Core.Services.Authentification.Token;

public class BearerTokenDispatcherService : IBearerTokenDispatcherService
{
    private IConfiguration configuration;

    public BearerTokenDispatcherService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<User> ExtractUserAsync(string tokenString)
    {
        return await Task.Run(() =>
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var signingKey = Encoding.UTF8.GetBytes(configuration["Safit:AuthService:Bearer:IssuerSigningKey"]!);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["Safit:AuthService:Bearer:ValidIssuer"],
                ValidAudience = configuration["Safit:AuthService:Bearer:ValidAudience"],
                ValidAlgorithms = new[] { "HS256" },
                IssuerSigningKey = new SymmetricSecurityKey(signingKey)
            };

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(tokenString.Substring(7), validationParameters, out var validatedToken);
                var jwtToken = validatedToken as JwtSecurityToken;
                var id = Convert.ToInt64(jwtToken?.Claims.FirstOrDefault(c => c.Type == "id")?.Value);
                var username = jwtToken?.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
                return new User() { Id = id, Username = username! };
            }
            catch
            {
                throw new UnauthorizedAccessException();
            }
        });
    }
}
