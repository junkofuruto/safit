using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class SportRepository : BaseRepository<Sport>, ISportRepository
{
    public SportRepository(DatabaseContext context) : base(context) { }
}