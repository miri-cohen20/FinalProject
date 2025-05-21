using System;
using System.Collections.Generic;

namespace Server.models;

public partial class Renting
{
    public int Id { get; set; }

    public string IdCar { get; set; } = null!;

    public string IdCustomer { get; set; } = null!;

    public DateTime RentalTime { get; set; }

    public DateTime ReturnTime { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public virtual Car IdCarNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
