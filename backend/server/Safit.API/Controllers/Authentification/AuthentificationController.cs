using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.Authentification;

[Route("api/v1/auth")]
[ApiController]
public class AuthentificationController : ControllerBase
{
    private IAuthorizationService authorizationService;

    public AuthentificationController(
        IAuthorizationService authorizationService)
    {
        this.authorizationService = authorizationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] AuthentificationLoginRequestContract request, CancellationToken ct)
    {
        try
        {
            var token = await authorizationService.LoginAsync(request.Username, request.Password, ct);
            var response = new AuthentificationLoginResponseContract() { Token = token.Value };
            return Ok(ResponseContract<AuthentificationLoginResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<AuthentificationLoginResponseContract>.Create(ex));
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AuthentificationRegisterRequestContract request, CancellationToken ct)
    {
        try
        {
            var token = await authorizationService.RegisterAsync(
                request.Email, request.Username, request.Password, 
                request.FirstName, request.LastName, request.ProfileSrc, ct);
            var response = new AuthentificationRegisterResponseContract() { Token = token.Value };
            return Ok(ResponseContract<AuthentificationRegisterResponseContract>.Create(response));
        }
        catch (Exception ex)
        {
            return BadRequest(ResponseContract<AuthentificationRegisterResponseContract>.Create(ex));
        }
    }
}
