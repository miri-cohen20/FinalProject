using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class RentalOrder
{
    public int Id { get; set; }

    public DateOnly Date { get; set; }

    public int TimeOfDay { get; set; }

    public int Car { get; set; }

    public bool Status { get; set; }
}
