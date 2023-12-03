using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Cart
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
