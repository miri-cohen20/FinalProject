using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class Worker
{
    public string Id { get; set; } = null!;

    public int HoursMonth { get; set; }

    public int RolsId { get; set; }

    public double Price { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual Role Rols { get; set; } = null!;
}
