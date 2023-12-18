namespace Safit.API.Controllers.Post;

public class PostGetByIdResponseContract
{
    public long Id { get; set; }  
    public long TrainerId { get; set; }
    public long SportId { get; set; }
    public int Views { get; set; }
    public string Content { get; set; } = null!;
    public IEnumerable<PostTagsContract> Tags { get; set; } = null!;
}
