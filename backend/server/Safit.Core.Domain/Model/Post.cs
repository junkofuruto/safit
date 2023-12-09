using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Post : Entity
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public string Content { get; set; } = null!;

    public long? CourseId { get; set; }

    public int Views { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Specialisation Specialisation { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
