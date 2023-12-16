namespace Safit.API.Controllers.Video;

public class VideoCreateCommentResponseContract
{
    public long Id { get; set; }
    public long? BranchId { get; set; }
    public string Value { get; set; } = null!;
}
