using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Tag : Entity
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
