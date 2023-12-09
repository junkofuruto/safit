using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class CourseAccess : Entity
{
    public long UserId { get; set; }

    public long CourseId { get; set; }

    public DateTime PurchaseDt { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
