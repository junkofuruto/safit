using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.Trainer;

[Authorize]
[Route("api/v1/trainer")]
[ApiController]
public class TrainerController : ControllerBase
{
    private ITrainerService trainerService;

    public TrainerController(ITrainerService trainerService)
    {
        this.trainerService = trainerService;
    }

    [HttpGet("{username}/videos")]
    public async Task<IActionResult> GetVideosAsync([FromRoute] string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{username}/courses")]
    public async Task<IActionResult> GetCoursesAsync([FromRoute] string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{username}/posts")]
    public async Task<IActionResult> GetPostsAsync([FromRoute] string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("{username}/products")]
    public async Task<IActionResult> GetProductsAsync([FromRoute] string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}