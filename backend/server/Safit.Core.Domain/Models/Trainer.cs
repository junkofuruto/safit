using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Trainer
{
    public long Id { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Timecode> Timecodes { get; set; } = new List<Timecode>();

    public virtual ICollection<TrainerSport> TrainerSports { get; set; } = new List<TrainerSport>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
