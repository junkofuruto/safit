using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Cart : Entity
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public virtual ICollection<CartContent> CartContents { get; set; } = new List<CartContent>();

    public virtual User User { get; set; } = null!;
}
