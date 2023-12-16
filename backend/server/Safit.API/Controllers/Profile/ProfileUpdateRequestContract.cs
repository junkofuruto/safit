namespace Safit.API.Controllers.Profile;

public class ProfileUpdateRequestContract
{
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ProfileSrc { get; set; } = null!;
    public string Email { get; set; } = null!;
}
