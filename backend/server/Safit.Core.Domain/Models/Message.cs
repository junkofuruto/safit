using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Message
{
    public long Id { get; set; }

    public long FromUserId { get; set; }

    public long DestUserId { get; set; }

    public virtual User DestUser { get; set; } = null!;

    public virtual User FromUser { get; set; } = null!;
}
