using Safit.Core.Domain.Model;

namespace Safit.API.Controllers.Trainer;

public class TrainerVideosResponseContract
{
    public long Id { get; set; }
    public long SportId { get; set; }
    public TrainerCoursesResponseContract? Courses { get; set; }
    public int Views {  get; set; }

}