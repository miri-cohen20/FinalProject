using System;
using System.Collections.Generic;

namespace Dal.models;

    public partial class Customer
    {
        // המזהה של הלקוח
//        public int Id { get; set; }


        public virtual User IdNavigation { get; set; } = null!;

        public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();

        // בוני או פעולה בפונקציה
      
    }

