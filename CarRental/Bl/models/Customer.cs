using System;
using System.Collections.Generic;

namespace Bl.models;

public partial class Customer
{
    public int Id { get; set; }

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
}
