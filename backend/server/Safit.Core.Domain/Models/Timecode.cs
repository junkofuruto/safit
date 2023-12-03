using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Timecode
{
    public long VideoId { get; set; }

    public TimeSpan Timing { get; set; }

    public long? ProductId { get; set; }

    public long? CourseId { get; set; }

    public long? PostId { get; set; }

    public long? TrainerId { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Post? Post { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Trainer? Trainer { get; set; }
}
