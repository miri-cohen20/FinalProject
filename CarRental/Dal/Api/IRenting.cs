using Dal.Api;
using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IRenting
    {

        List<Renting> GetAllRenting();
        void UpdateEndRental( int idRenting);
        bool UpdateReturnTimeRenting(int idRenting, DateTime returnTime, double price);
        bool AddRenting( string idCar, string idCustomer, DateTime rentalTime, DateTime returnTime, double price);
    }
}




//public int Id { get; set; }

//public int IdCar { get; set; }

//public int IdCustomer { get; set; }

//public DateTime RentalTime { get; set; }

//public DateTime ReturnTime { get; set; }

//public double Price { get; set; }

//public virtual Car IdCarNavigation { get; set; } = null!;

//public virtual Customer IdCustomerNavigation { get; set; } = null!;
//}