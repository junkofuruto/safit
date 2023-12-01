using Microsoft.AspNetCore.Mvc;
using Safit.Core.Services.Auth;

namespace Safit.API.Controllers.Authentification
{
    [Route("api/auth")]
    public class AuthentificationController : Controller
    {
        private IBearerTokenGeneratorService bearerTokenGeneratorService;

        public AuthentificationController(
            IBearerTokenGeneratorService bearerTokenGeneratorService)
        {
            this.bearerTokenGeneratorService = bearerTokenGeneratorService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthentificationLoginRequestContract request)
        {
            return Ok(new ResponseContract<AuthentificationLoginResponseContract>()
            {
                Success = true,
                Message = string.Empty,
                Value = new AuthentificationLoginResponseContract()
                {
                    Id = 0,
                    Token = await bearerTokenGeneratorService.GenerateAsync(0),
                    Username = "admin"
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthentificationRegisterRequestContract request)
        {
            return Ok(new ResponseContract<AuthentificationRegisterResponseContract>()
            {
                Success = true,
                Message = string.Empty,
                Value = new AuthentificationRegisterResponseContract()
                {
                    Id = 0,
                    Token = await bearerTokenGeneratorService.GenerateAsync(0),
                    Username = "admin"
                }
            });
        }
    }
}
