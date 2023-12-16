namespace Safit.API.Controllers.Video;

public class VideoCreateCommentRequestContract
{
    public long? BranchId { get; set; }
    public string Text { get; set; } = null!;
}
