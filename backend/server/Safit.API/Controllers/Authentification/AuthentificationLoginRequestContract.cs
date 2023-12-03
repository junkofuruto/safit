namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationLoginRequestContract
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}