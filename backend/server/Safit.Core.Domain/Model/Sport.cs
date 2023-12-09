using System;
using System.Collections.Generic;

namespace Safit.Core.Domain.Model;

public partial class Sport : Entity
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string PreviewSrc { get; set; } = null!;

    public virtual ICollection<Specialisation> Specialisations { get; set; } = new List<Specialisation>();
}
