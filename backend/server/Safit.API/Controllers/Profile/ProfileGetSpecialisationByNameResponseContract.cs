namespace Safit.API.Controllers.Profile;

public class ProfileGetSpecialisationByNameResponseContract
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PreviewSrc { get; set; } = null!;
}
