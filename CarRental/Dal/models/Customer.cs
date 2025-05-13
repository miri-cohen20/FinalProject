using System;
using System.Collections.Generic;

namespace Dal.models;

    public partial class Customer
    {

    private int _id; // שדה פרטי לאחסון ה-ID

    public int Id
    {
        get => _id;
        set
        {
            _id = value;
            if (IdNavigation != null)
            {
                IdNavigation.Id = value; // קבע את ה-ID של IdNavigation
            }
        }
    }


    public virtual User IdNavigation { get; set; } = null!;

        public virtual ICollection<Renting> Rentings { get; set; } = new List<Renting>();

        // בוני או פעולה בפונקציה
      
    }

