using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(DatabaseContext context) : base(context) { }
}