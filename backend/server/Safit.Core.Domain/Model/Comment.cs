using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Comment : Entity
{
    public long Id { get; set; }

    public long VideoId { get; set; }

    public long UserId { get; set; }

    public long? BranchId { get; set; }

    public virtual Comment? Branch { get; set; }

    public virtual ICollection<Comment> InverseBranch { get; set; } = new List<Comment>();

    public virtual User User { get; set; } = null!;

    public virtual Video Video { get; set; } = null!;
}
