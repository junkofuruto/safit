using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Attachment
{
    public long Id { get; set; }

    public long MessageId { get; set; }

    public virtual Message Message { get; set; } = null!;
}
