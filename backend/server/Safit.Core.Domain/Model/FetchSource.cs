using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class FetchSource : Entity
{
    public long VideoId { get; set; }

    public string Source { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
