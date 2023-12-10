using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class CartContentRepository : BaseRepository<CartContent>, ICartContentRepository
{
    public CartContentRepository(DatabaseContext context) : base(context) { }
}