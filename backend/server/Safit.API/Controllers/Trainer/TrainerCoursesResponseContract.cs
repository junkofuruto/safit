namespace Safit.API.Controllers.Trainer;

public class TrainerCoursesResponseContract
{
    public long Id { get; set; }
    public long SportId { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}