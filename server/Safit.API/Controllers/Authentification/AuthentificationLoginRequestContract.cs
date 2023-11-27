namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationLoginRequestContract
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}