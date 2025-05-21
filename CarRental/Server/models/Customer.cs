using System;
using System.Collections.Generic;

namespace Server.models;

public partial class Customer
{
    public string Id { get; set; } = null!;

    public virtual User IdNavigation { get; set; } = null!;

    public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
}
