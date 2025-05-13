using System;
using System.Collections.Generic;

namespace Bl.models;

public partial class Worker
{
    public int Id { get; set; }

    public int HoursMonth { get; set; }

    public int RolsId { get; set; }

    public double Price { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual Role Rols { get; set; } = null!;
}
