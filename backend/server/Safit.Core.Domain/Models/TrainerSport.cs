using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class TrainerSport
{
    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Sport Sport { get; set; } = null!;

    public virtual Trainer Trainer { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
