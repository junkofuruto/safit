using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.Sport;

[Authorize]
[Route("api/v1/sport")]
[ApiController]
public class SportController : ControllerBase
{
    private ITrainerService trainerService;

    public SportController(ITrainerService trainerService)
    {
        this.trainerService = trainerService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] SportCreateRequestContract request)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public async Task<IActionResult> FindAsync(
        [FromQuery(Name = "q")] string query,
        [FromQuery(Name = "c")] long count,
        [FromQuery(Name = "off")] long offset,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
