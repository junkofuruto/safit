namespace Safit.API.Controllers.Sport;

public class SportCreateRequestContract
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PreviewSrc { get; set; } = null!;
}
