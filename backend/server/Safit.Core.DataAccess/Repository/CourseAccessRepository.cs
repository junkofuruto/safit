using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class CourseAccessRepository : BaseRepository<CourseAccess>, ICourseAccessRepository
{
    public CourseAccessRepository(DatabaseContext context) : base(context) { }
}