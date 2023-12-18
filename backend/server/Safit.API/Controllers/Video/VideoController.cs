using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Safit.API.Controllers.Video;

[Route("api/v1/video")]
[ApiController]
public class VideoController : ControllerBase
{
    [HttpGet("rec")]
    public async Task<IActionResult> GetRecommendedAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/playback")]
    public async Task<IActionResult> GetPlaybackByIdAsync([FromRoute] long id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/like")]
    public async Task<IActionResult> ToggleLikeAsync([FromRoute] long id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id}/comment")]
    public async Task<IActionResult> CreateCommentAsync(
        [FromRoute] long id,
        [FromBody] VideoCreateCommentRequestContract request,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id}/comment")]
    public async Task<IActionResult> GetVideoCommentsAsync(
        [FromRoute] long id,
        [FromQuery(Name = "c")] int count,
        [FromQuery(Name = "off")] int offset,
        [FromQuery(Name = "org")] long origin,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("{id}/upload")]
    public async Task<IActionResult> UploadAsync([FromRoute] long id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
