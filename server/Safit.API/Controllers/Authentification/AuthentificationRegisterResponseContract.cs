namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationRegisterResponseContract : ResponseBase
{
    public long Id { get; set; }
    public string? Token { get; set; }
    public string? Username { get; set; }
}