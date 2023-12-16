namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationRegisterRequestContract
{
    public string? Email { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? ProfileSrc { get; set; }
}