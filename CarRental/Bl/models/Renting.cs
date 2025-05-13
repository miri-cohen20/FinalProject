using System;
using System.Collections.Generic;

namespace Bl.models;

public partial class Renting
{
    public int Id { get; set; }

    public int IdCar { get; set; }

    public int IdCustomer { get; set; }

    public DateTime RentalTime { get; set; }

    public DateTime ReturnTime { get; set; }

    public double Price { get; set; }

    public bool Available { get; set; }

    public virtual Car IdCarNavigation { get; set; } = null!;

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
