using System;
using System.Collections.Generic;

namespace Dal.models;

public partial class Customer:User
{
  //  public int Id { get; set; }

   // public string FirstName { get; set; } = null!;

   // public string? LastName { get; set; }

   // public int PhonNumber { get; set; }

   // public string? Email { get; set; }

  //  public string City { get; set; } = null!;

  //  public string Street { get; set; } = null!;

   // public int? BuildingNumber { get; set; }

    public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
}
