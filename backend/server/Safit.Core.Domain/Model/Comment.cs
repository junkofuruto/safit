using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Comment : Entity
{
    public long Id { get; set; }

    public long VideoId { get; set; }

    public long UserId { get; set; }

    public long? BranchId { get; set; }

    public string? Value { get; set; }

    public virtual Comment? Branch { get; set; }

    public virtual ICollection<Comment> InverseBranch { get; set; } = new List<Comment>();
}
