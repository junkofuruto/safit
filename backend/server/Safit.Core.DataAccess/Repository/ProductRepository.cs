using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DatabaseContext context) : base(context) { }
}