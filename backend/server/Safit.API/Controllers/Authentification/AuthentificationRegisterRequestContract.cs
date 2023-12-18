namespace Safit.API.Controllers.Authentification;

public sealed class AuthentificationRegisterRequestContract
{
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? ProfileSrc { get; set; }
}