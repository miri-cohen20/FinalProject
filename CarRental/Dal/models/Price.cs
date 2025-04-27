using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class Price
{
    public int Id { get; set; }

    public int Seats { get; set; }

    public int Time { get; set; }

    public double PriceForHour { get; set; }
}
