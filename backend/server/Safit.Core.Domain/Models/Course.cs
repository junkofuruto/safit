using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Course
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Timecode> Timecodes { get; set; } = new List<Timecode>();

    public virtual TrainerSport TrainerSport { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
