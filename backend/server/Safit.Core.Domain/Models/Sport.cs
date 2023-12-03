using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Sport
{
    public long Id { get; set; }

    public virtual ICollection<TrainerSport> TrainerSports { get; set; } = new List<TrainerSport>();
}
