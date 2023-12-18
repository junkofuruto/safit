namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationLoginRequestContract
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}