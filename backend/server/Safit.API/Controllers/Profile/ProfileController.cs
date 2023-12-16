using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.Profile;

[Authorize]
[Route("api/v1/profile")]
[ApiController]
public class ProfileController : ControllerBase
{
    private IProfileService profileService;

    public ProfileController(IProfileService profileService)
    {
        this.profileService = profileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] ProfileUpdateRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("community/{username}")]
    public async Task<IActionResult> GetByNameAsync([FromRoute] string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("community/{username}/specialisation")]
    public async Task<IActionResult> GetSpecialisationsByNameAsync([FromRoute] string username, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPatch("promote")]
    public async Task<IActionResult> PromoteAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("specialisation")]
    public async Task<IActionResult> GetSpecialisationsAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("specialisation")]
    public async Task<IActionResult> CreateSpecialisationsAsync([FromBody] ProfileCreateSpecialisationRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync(
        [FromQuery(Name = "q")] string query,
        [FromQuery(Name = "c")] long count,
        [FromQuery(Name = "off")] long offset,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}