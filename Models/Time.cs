using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Time
{
    public int Id { get; set; }

    public string Duration { get; set; } = null!;
}
