namespace Safit.Core.Domain.Service.Entities;

public class ProfileModificationData
{
    public string Password { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string ProfileSrc { get; set; } = null!;
}