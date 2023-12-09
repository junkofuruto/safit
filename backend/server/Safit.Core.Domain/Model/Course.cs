using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Course : Entity
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public decimal Price { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CourseAccess> CourseAccesses { get; set; } = new List<CourseAccess>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Specialisation Specialisation { get; set; } = null!;

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
