using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface ISighUp
    {
        public bool CreateCustomer(Customer user);
        public bool CreateCustomer(string id, string firstName, string password, string phoneNumber, string city, string street, int buildingNumber = 0, string lastName = null, string email = null);
        bool CreateWorker(string id, string firstName, string password, string phoneNumber, string city, string street, int hoursMonth, int roleId, int buildingNumber = 0, string lastName = null, string email = null);
        bool CreateWorker(string id, int hoursMonth, int roleId);



    }
}
//public int Id { get; set; }

//public int HoursMonth { get; set; }

//public int RolsId { get; set; }

//public virtual User IdNavigation { get; set; } = null!;

//public virtual Role Rols { get; set; } = null!;