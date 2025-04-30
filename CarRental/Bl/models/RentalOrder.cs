using System;
using System.Collections.Generic;

namespace Bl.models;

public partial class RentalOrder
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TimeOfDay { get; set; }

    public int Car { get; set; }

    public bool Status { get; set; }
}
