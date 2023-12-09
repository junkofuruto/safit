using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Recommendation : Entity
{
    public long UserId { get; set; }

    public long TagId { get; set; }

    public float Weight { get; set; }

    public virtual Tag Tag { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
