using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Worker:User
{
    public int UserId { get; set; }

    public int HoursMonth { get; set; }

    public int RolsId { get; set; }

    public virtual Role Rols { get; set; } = null!;

   // public virtual User User { get; set; } = null!;
}
