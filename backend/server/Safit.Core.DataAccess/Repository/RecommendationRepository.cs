using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class RecommendationRepository : BaseRepository<Recommendation>, IRecommendationRepository
{
    public RecommendationRepository(DatabaseContext context) : base(context) { }
}