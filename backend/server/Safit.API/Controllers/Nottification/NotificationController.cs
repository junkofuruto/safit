using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Safit.API.Controllers.Nottification
{
    [Authorize]
    [Route("ntf")]
    public class NotificationController : Controller
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Notify()
        {
            throw new NotImplementedException();
        }

        [HttpGet("subscribe")]
        public async Task<IActionResult> Subscribe()
        {
            throw new NotImplementedException();
        }
    }
}
