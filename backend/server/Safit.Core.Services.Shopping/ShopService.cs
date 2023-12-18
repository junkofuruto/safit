using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Service;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Services.Shopping;

public class ShopService : IShopService
{
    private IAuthentificationService authentificationService;
    private IRepositoryWrapper repositoryWrapper;

    public ShopService(IAuthentificationService authentificationService, IRepositoryWrapper repositoryWrapper)
    {
        this.authentificationService = authentificationService;
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<IQueryable<CartContent>> GetCartContent(AuthentificationToken token, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var cartInstance = (await repositoryWrapper.Cart.FindByCondition(x => x.UserId == userInfo.Id)).Select(x => x.Id).Max();
        return await repositoryWrapper.CartContent.FindByCondition(x => x.CartId == cartInstance);
    }

    public async Task<Product> GetProductInfoAsync(long id, CancellationToken ct = default)
    {
        var products = await repositoryWrapper.Product.FindByCondition(x => x.Id == id);
        if (products.Any() == false) throw new ArgumentException("no product with such id");
        return products.First();
    }

    public async Task<IQueryable<CartContent>> UpdateCartContent(AuthentificationToken token, long productId, int amount, CancellationToken ct = default)
    {
        var userInfo = await authentificationService.ExtractAsync(token, ct);
        var cartInstance = (await repositoryWrapper.Cart.FindByCondition(x => x.UserId == userInfo.Id)).Select(x => x.Id).Max();
        var cartContent = (await repositoryWrapper.CartContent.FindByCondition(x => x.ProductId == productId && x.CartId == cartInstance)).First();
        cartContent.Amount = amount;
        await repositoryWrapper.CartContent.Update(cartContent, ct);
        await repositoryWrapper.SaveChangesAsync();
        return await repositoryWrapper.CartContent.FindByCondition(x => x.ProductId == productId && x.CartId == cartInstance);
    }
}
