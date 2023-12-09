using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class CartContent : Entity
{
    public long CartId { get; set; }

    public long ProductId { get; set; }

    public int Amount { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
