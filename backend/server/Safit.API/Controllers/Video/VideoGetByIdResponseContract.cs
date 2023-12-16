namespace Safit.API.Controllers.Video;

public class VideoGetByIdResponseContract
{
    public long Id { get; set; }
    public long TrainerId { get; set; }
    public long SportId { get; set; }
    public long Views { get; set; }
    public long Likes { get; set; }
    public long Comments { get; set; }
    public IEnumerable<VideoTagContract> Tags { get; set; } = null!;
}
