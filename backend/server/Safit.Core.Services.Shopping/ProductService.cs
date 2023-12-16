using Microsoft.EntityFrameworkCore;
using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;
using Safit.Core.Domain.Repository.Exceptions;
using Safit.Core.Domain.Service;

namespace Safit.Core.Services.Shopping;

public class ProductService : IProductService
{
    private IRepositoryWrapper repositoryWrapper;

    public ProductService(IRepositoryWrapper repositoryWrapper)
    {
        this.repositoryWrapper = repositoryWrapper;
    }

    public async Task<IQueryable<Product>> GetProductsAsync(Trainer trainer, CancellationToken ct = default)
    {
        var trainers = (await repositoryWrapper.Trainer.FindByCondition(x => x.Id == trainer.Id, ct)).First();
        var products = await repositoryWrapper.Product.FindByCondition(x => x.TrainerId == trainers.Id, ct);
        return products;
    }
    public async Task<CartContent> AddProductAsync(Trainer user, Product product, CancellationToken ct = default)
    {
        var cartInstances = (await repositoryWrapper.Cart.FindByCondition(x => user.Id == x.UserId, ct)).Select(x => x.Id);
        if (cartInstances.Any() == false) throw new ArgumentNullException("No cart instances found");
        var cartContent = new CartContent() { CartId = cartInstances.Max(), ProductId = product.Id };
        await repositoryWrapper.CartContent.Create(cartContent, ct);
        await repositoryWrapper.SaveChangesAsync();
        return cartContent;
    }
    public async Task<Product> GetProductInfoAsync(long id, CancellationToken ct = default)
    {
        var selection = await repositoryWrapper.Product.FindByCondition(x => x.Id == id, ct);
        var product = await selection.FirstOrDefaultAsync(ct);
        if (product == null) throw new EntityNullException();
        return product;
    }
    public async Task<IQueryable<CartContent>> GetCartContentAsync(User user, CancellationToken ct = default)
    {
        var cartInstances = (await repositoryWrapper.Cart.FindByCondition(x => user.Id == x.UserId, ct)).Select(x => x.Id);
        if (cartInstances.Any() == false) throw new ArgumentNullException("No cart instances found");
        return await repositoryWrapper.CartContent.FindByCondition(x => x.CartId == cartInstances.Max(), ct);
    }
    public async Task<Cart> CreateNewCartInstanceAsync(User user, CancellationToken ct = default)
    {
        var cart = new Cart() { UserId = user.Id };
        await repositoryWrapper.Cart.Create(cart, ct);
        await repositoryWrapper.SaveChangesAsync();
        return cart;
    }
}