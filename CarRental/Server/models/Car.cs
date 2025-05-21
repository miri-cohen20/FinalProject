using System;
using System.Collections.Generic;

namespace Server.models;

public partial class Car
{
    public string Id { get; set; } = null!;

    public int Seats { get; set; }

    public bool CleanStatus { get; set; }

    public bool ProperStatus { get; set; }

    public DateTime FinalCleaning { get; set; }

    public DateTime LastCorrection { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string DescriptionCleaning { get; set; } = null!;

    public string DescriptionProper { get; set; } = null!;

    public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
}
