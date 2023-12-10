using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

internal class CourseAccessRepository : BaseRepository<CourseAccess>, ICourseAccessRepository
{
    public CourseAccessRepository(DatabaseContext context) : base(context) { }
}