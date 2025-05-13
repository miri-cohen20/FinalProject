using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Price
{
    public int Id { get; set; }

    public int Seats { get; set; }

    public int Time { get; set; }

    public double Price1 { get; set; }
}
