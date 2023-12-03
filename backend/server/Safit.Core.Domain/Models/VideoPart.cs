using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class VideoPart
{
    public long PartId { get; set; }

    public long VideoId { get; set; }

    public string Source { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
