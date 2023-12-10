using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(DatabaseContext context) : base(context) { }
}