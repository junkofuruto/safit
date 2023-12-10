using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class TrainerRepository : BaseRepository<Trainer>, ITrainerRepository
{
    public TrainerRepository(DatabaseContext context) : base(context) { }
}