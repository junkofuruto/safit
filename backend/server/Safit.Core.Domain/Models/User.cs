using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class User
{
    public long Id { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Message> MessageDestUsers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageFromUsers { get; set; } = new List<Message>();

    public virtual Trainer? Trainer { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
