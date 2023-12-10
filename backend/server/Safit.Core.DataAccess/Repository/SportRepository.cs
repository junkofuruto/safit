using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class SportRepository : BaseRepository<Sport>, ISportRepository
{
    public SportRepository(DatabaseContext context) : base(context) { }
}