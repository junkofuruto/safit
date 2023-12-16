namespace Safit.API.Controllers.Profile;

public class ProfileGetResponseContract
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ProfileSrc { get; set; } = null!;
    public decimal Balance { get; set; }
    public bool IsTrainer { get; set; }
}
