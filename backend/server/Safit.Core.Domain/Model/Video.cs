using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Video : Entity
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public long? CourseId { get; set; }

    public int Views { get; set; }

    public bool Visible { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Course? Course { get; set; }

    public virtual ICollection<FetchSource> FetchSources { get; set; } = new List<FetchSource>();

    public virtual Specialisation Specialisation { get; set; } = null!;

    public virtual ICollection<Tag> Tags { get; set; } = new List<Tag>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
