using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Safit.API.Controllers.Shop;

[Authorize]
[Route("api/v1/shop")]
[ApiController]
public class ShopController : ControllerBase
{
    [HttpGet("product/{id}")] 
    public async Task<IActionResult> GetProductAsync([FromRoute] long id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPost("product")]
    public async Task<IActionResult> CreateProductAsync([FromBody] ShopCreateProductRequestContract request, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpGet("cart")]
    public async Task<IActionResult> GetCartAsync(CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    [HttpPut("product/{id}")]
    public async Task<IActionResult> UpdateCartAsync(
        [FromRoute] long id,
        [FromQuery(Name = "a")] int amount,
        CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}
