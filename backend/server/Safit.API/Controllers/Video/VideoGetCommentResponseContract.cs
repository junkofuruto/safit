namespace Safit.API.Controllers.Video;

public class VideoGetCommentResponseContract
{
    public long Id { get; set; }
    public string Value { get; set; } = null!;
    public bool HasBranch { get; set; }
}
