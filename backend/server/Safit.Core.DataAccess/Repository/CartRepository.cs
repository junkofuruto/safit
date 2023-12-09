using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    public CartRepository(DatabaseContext context) : base(context) { }
}