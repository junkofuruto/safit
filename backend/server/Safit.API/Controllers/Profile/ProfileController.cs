using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;
using Safit.Core.Domain.Service.Entities;

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
        try
        {
            var token = new AuthentificationToken() { Value = Request.Headers.Authorization };
            var post = await profileService.GetFullInfoAsync(token, ct);
            var response = post.Adapt<ProfileGetResponseContract>();
            return BadRequest(ResponseContract<ProfileGetResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<ProfileGetResponseContract>.Create(ex));
        }
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] ProfileUpdateRequestContract request, CancellationToken ct)
    {
        try
        {
            var token = new AuthentificationToken() { Value = Request.Headers.Authorization };
            var modificationData = new ProfileModificationData()
            {
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                ProfileSrc = request.ProfileSrc
            };
            var post = await profileService.UpdateDataAsync(token, modificationData, ct);
            var response = post.Adapt<ProfileUpdateResponseContract>();
            return BadRequest(ResponseContract<ProfileUpdateResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<ProfileUpdateResponseContract>.Create(ex));
        }
    }

    [AllowAnonymous]
    [HttpGet("community/{username}")]
    public async Task<IActionResult> GetByNameAsync([FromRoute] string username, CancellationToken ct)
    {
        try
        {
            var post = await profileService.GetInfoAsync(username, ct);
            var response = post.Adapt<ProfileGetByNameResponseContract>();
            return BadRequest(ResponseContract<ProfileGetByNameResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<ProfileGetByNameResponseContract>.Create(ex));
        }
    }

    [HttpPatch("promote")]
    public async Task<IActionResult> PromoteAsync(CancellationToken ct)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
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