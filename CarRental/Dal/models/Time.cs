﻿using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class Time
{
    public int Id { get; set; }

    public string Duration { get; set; } = null!;
}
