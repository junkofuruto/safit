using Microsoft.AspNetCore.Mvc;
using Safit.Core.Domain.Service;

namespace Safit.API.Controllers.Authentification;

[Route("api/v1/auth")]
[ApiController]
public class AuthentificationController : ControllerBase
{
    private IAuthentificationService authentificationService;

    public AuthentificationController(IAuthentificationService authentificationService)
    {
        this.authentificationService = authentificationService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] AuthentificationLoginRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AuthentificationRegisterRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
