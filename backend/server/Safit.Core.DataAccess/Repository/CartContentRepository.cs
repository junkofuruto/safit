using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

public class CartContentRepository : BaseRepository<CartContent>, ICartContentRepository
{
    public CartContentRepository(DatabaseContext context) : base(context) { }
}