using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Role
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public double PriceHour { get; set; }

    public virtual ICollection<Worker> Workers { get; set; } = new List<Worker>();
}
