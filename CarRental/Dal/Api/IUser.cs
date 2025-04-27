using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.models;

namespace Dal.Api
{
    internal interface IUser
    {
        User GetUser(int id);
        List<User> GetAllUsers();



    }
}
//public int Id { get; set; }

//public string FirstName { get; set; } = null!;

//public string? LastName { get; set; }

//public int PhonNumber { get; set; }

//public string? Email { get; set; }

//public string City { get; set; } = null!;

//public string Street { get; set; } = null!;

//public int? BuildingNumber { get; set; }

//public virtual Customer? Customer { get; set; }

//public virtual Worker? Worker { get; set; }