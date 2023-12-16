namespace Safit.API.Controllers.Profile;

public class ProfileGetByNameResponseContract
{
    public long Id { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ProfileSrc { get; set; } = null!;
    public bool IsTrainer { get; set; }
}