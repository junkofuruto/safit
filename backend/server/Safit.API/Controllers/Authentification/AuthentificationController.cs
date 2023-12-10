using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.Authentification;

[Route("api/v1/auth")]
public class AuthentificationController : Controller
{
    private IBearerTokenGeneratorService bearerTokenGeneratorService;
    private IAuthorisationService authorisationService;

    public AuthentificationController(
        IBearerTokenGeneratorService bearerTokenGeneratorService,
        IAuthorisationService authorisationService)
    {
        this.bearerTokenGeneratorService = bearerTokenGeneratorService;
        this.authorisationService = authorisationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] AuthentificationLoginRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
        //try
        //{
        //    var user = await authorisationService.LoginAsync(request.Username!, request.Password!, ct);
        //    return Ok(ResponseContract<AuthentificationLoginResponseContract>.Create(
        //        new AuthentificationLoginResponseContract()
        //        {
        //            Token = await bearerTokenGeneratorService.GenerateAsync(user),
        //            Username = user.Username
        //        }));
        //}
        //catch (Exception ex)
        //{
        //    return BadRequest(ResponseContract<AuthentificationLoginResponseContract>.Create(ex));
        //}
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AuthentificationRegisterRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
        //try
        //{
        //    var user = await authorisationService.RegisterAsync(request.Username!, request.Password!, ct);
        //    return Ok(ResponseContract<AuthentificationRegisterResponseContract>.Create(
        //        new AuthentificationRegisterResponseContract()
        //        {
        //            Token = await bearerTokenGeneratorService.GenerateAsync(user),
        //            Username = user.Username
        //        }));
        //}
        //catch (Exception ex) { return BadRequest(ResponseContract<AuthentificationRegisterResponseContract>.Create(ex)); }
    }
}
