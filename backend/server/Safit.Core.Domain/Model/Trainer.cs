using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Trainer : Entity
{
    public long Id { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Specialisation> Specialisations { get; set; } = new List<Specialisation>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
