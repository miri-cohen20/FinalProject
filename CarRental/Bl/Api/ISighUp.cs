using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    internal interface ISighUp
    {
        bool CreateCustomer(int id, string firstName, int phoneNumber, string city, string street, int buildingNumber = 0, string lastName=null,  string email= null);
        bool CreateWorker(int id, string firstName, string password, int phoneNumber, string city, string street, int hoursMonth, int roleId, int buildingNumber = 0, string lastName = null, string email = null);
        bool CreateWorker(int id, int hoursMonth, int roleId);



    }
}
//public int Id { get; set; }

//public int HoursMonth { get; set; }

//public int RolsId { get; set; }

//public virtual User IdNavigation { get; set; } = null!;

//public virtual Role Rols { get; set; } = null!;