using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Video
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public long? CourseId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Course? Course { get; set; }

    public virtual TrainerSport TrainerSport { get; set; } = null!;

    public virtual ICollection<VideoPart> VideoParts { get; set; } = new List<VideoPart>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
