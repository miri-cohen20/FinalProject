using System;
using System.Collections.Generic;

namespace Dal.Models;

public partial class Customer:User
{
   // public int IdCustomers { get; set; }

   /// public virtual User IdCustomersNavigation { get; set; } = null!;

    public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();
}
