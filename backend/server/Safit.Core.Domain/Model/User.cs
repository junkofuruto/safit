using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class User : Entity
{
    public long Id { get; set; }

    public string Email { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? ProfileSrc { get; set; }

    public decimal Balance { get; set; }

    public string Token { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<CourseAccess> CourseAccesses { get; set; } = new List<CourseAccess>();

    public virtual ICollection<Recommendation> Recommendations { get; set; } = new List<Recommendation>();

    public virtual Trainer? Trainer { get; set; }

    public virtual ICollection<Trainer> Trainers { get; set; } = new List<Trainer>();

    public virtual ICollection<Video> Videos { get; set; } = new List<Video>();
}
