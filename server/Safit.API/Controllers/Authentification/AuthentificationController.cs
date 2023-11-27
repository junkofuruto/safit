using Microsoft.AspNetCore.Mvc;

namespace Safit.API.Controllers.Authentification
{
    [Route("api/auth")]
    public class AuthentificationController : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] AuthentificationLoginRequestContract request)
        {
            return await Task.Run(() => Ok(new ResponseContract<AuthentificationLoginResponseContract>()
            {
                Success = true,
                Message = string.Empty,
                Value = new AuthentificationLoginResponseContract()
                {
                    Id = 0,
                    Token = "aksdjas",
                    Username = "admin"
                }
            }));
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] AuthentificationRegisterRequestContract request)
        {
            return await Task.Run(() => Ok(new ResponseContract<AuthentificationRegisterResponseContract>()
            {
                Success = true,
                Message = string.Empty,
                Value = new AuthentificationRegisterResponseContract() 
                { 
                    Id = 0,
                    Token = "asdasd",
                    Username = "admin"
                }
            }));
        }
    }
}
