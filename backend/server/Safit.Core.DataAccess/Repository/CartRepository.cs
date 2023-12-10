using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(DatabaseContext context) : base(context) { }
}