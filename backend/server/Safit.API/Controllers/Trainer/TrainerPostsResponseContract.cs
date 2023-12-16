namespace Safit.API.Controllers.Trainer;

public class TrainerPostsResponseContract
{
    public long Id { get; set; }
    public string Views { get; set; } = null!;
    public string Content { get; set; } = null!;
    public TrainerCoursesResponseContract? Course { get; set; }
}
