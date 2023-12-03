using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Post
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public long? CourseId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Timecode> Timecodes { get; set; } = new List<Timecode>();

    public virtual TrainerSport TrainerSport { get; set; } = null!;
}
