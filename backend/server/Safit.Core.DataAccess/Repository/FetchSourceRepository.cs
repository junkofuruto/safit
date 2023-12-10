using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class FetchSourceRepository : BaseRepository<FetchSource>, IFetchSourceRepository
{
    public FetchSourceRepository(DatabaseContext context) : base(context) { }
}