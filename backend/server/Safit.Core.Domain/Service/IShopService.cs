using Safit.Core.Domain.Model;
using Safit.Core.Domain.Service.Authentification;

namespace Safit.Core.Domain.Service;

public interface IShopService
{
    public Task<Product> GetProductInfoAsync(long id, CancellationToken ct = default);
    public Task<Product> GetProductById(AuthentificationToken token, Product product, CancellationToken ct = default);
    public Task<IQueryable<CartContent>> GetCartContent(AuthentificationToken token, CancellationToken ct = default);
    public Task<IQueryable<CartContent>> UpdateCartContent(AuthentificationToken token, long productId, int amount, CancellationToken ct = default);
}