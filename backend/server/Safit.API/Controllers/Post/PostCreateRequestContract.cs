namespace Safit.API.Controllers.Post;

public class PostCreateRequestContract
{
    public long SportId { get; set; }
    public long? CourseId { get; set; }
    public string Content { get; set; } = null!;
}
