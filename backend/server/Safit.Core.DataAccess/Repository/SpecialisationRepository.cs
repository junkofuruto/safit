using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class SpecialisationRepository : BaseRepository<Specialisation>, ISpecialisationRepository
{
    public SpecialisationRepository(DatabaseContext context) : base(context) { }
}