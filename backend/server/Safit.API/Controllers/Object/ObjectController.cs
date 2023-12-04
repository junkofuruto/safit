using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Safit.API.Controllers.Object;

[Authorize]
[Route("obj")]
public class ObjectController : Controller
{
    [HttpGet]
    public async Task<IActionResult> GetObjectByName([FromQuery] string id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}