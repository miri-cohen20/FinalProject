using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Car
{
    public int Id { get; set; }

    public int Seats { get; set; }

    public bool Status { get; set; }

    public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
}
