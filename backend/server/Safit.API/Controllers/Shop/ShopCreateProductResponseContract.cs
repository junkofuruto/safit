namespace Safit.API.Controllers.Shop;

public class ShopCreateProductResponseContract
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public long SportId { get; set; }
    public string Link { get; set; } = null!;
    public decimal Price { get; set; }
}
