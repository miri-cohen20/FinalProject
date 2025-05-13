using System;
using System.Collections.Generic;

namespace Bl.models;

public partial class Price
{
    public int Id { get; set; }

    public int Seats { get; set; }

    public int Time { get; set; }

    public double PriceForHour { get; set; }
}
