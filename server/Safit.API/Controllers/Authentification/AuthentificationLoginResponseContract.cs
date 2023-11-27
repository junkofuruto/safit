namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationLoginResponseContract : ResponseBase
{
    public long Id { get; set; }
    public string? Token { get; set; }
    public string? Username { get; set; }
}