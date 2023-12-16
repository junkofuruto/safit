namespace Safit.API.Controllers.Trainer;

public class TrainerProductsResponseContract
{
    public long Id { get; set; }
    public string Link { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}