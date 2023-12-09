using Safit.Core.Domain.Model;

namespace Safit.Core.DataAccess.Repository;

public class CourseRepository : BaseRepository<Course>, ICourseRepository
{
    public CourseRepository(DatabaseContext context) : base(context) { }
}