using System;
using System.Collections.Generic;

namespace Dal.models;

    public partial class Customer
    {
        
        public int Id
         { get
                 => Id;
             set => Id = IdNavigation.Id;
         }


        public virtual User IdNavigation { get; set; } = null!;

        public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();

        // בוני או פעולה בפונקציה
      
    }

