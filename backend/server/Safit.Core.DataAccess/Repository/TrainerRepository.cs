using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class TrainerRepository : BaseRepository<Trainer>, ITrainerRepository
{
    public TrainerRepository(DatabaseContext context) : base(context) { }
}