using Safit.Core.Domain.Model;
using Safit.Core.Domain.Repository;

namespace Safit.Core.DataAccess.Repository;

internal class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(DatabaseContext context) : base(context) { }
}