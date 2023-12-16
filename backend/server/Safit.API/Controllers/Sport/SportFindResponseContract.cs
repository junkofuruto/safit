namespace Safit.API.Controllers.Sport;

public class SportFindResponseContract
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string PrewiewSrc { get; set; } = null!;
}
