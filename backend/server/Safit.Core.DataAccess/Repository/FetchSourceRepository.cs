using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class FetchSourceRepository : BaseRepository<FetchSource>, IFetchSourceRepository
{
    public FetchSourceRepository(DatabaseContext context) : base(context) { }
}