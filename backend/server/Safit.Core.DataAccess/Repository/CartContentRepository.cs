using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class CartContentRepository : BaseRepository<CartContent>, ICartContentRepository
{
    public CartContentRepository(DatabaseContext context) : base(context) { }
}