using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Product : Entity
{
    public long Id { get; set; }

    public long TrainerId { get; set; }

    public long SportId { get; set; }

    public string Link { get; set; } = null!;

    public decimal Price { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<CartContent> CartContents { get; set; } = new List<CartContent>();

    public virtual Specialisation Specialisation { get; set; } = null!;
}
