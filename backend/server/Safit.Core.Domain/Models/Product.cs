using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Models;

public partial class Product
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public virtual ICollection<Timecode> Timecodes { get; set; } = new List<Timecode>();

    public virtual TrainerSport TrainerSport { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
