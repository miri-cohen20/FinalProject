using Dal.models;
using System.Collections.Generic;
using YourNamespace;

namespace Bl.CustomerInput
{
    public class CustomerRenting
    {
        // Properties required for registration
        public int Id { get; set; }

        public string IdCar { get; set; }

        public string IdCustomer { get; set; }

        public DateTime RentalTime { get; set; }

        public DateTime ReturnTime { get; set; }

        public double Price { get; set; }

        public bool Available { get; set; }

        public static CustomerRenting FromRenting(Renting renting)
        {
            return new CustomerRenting
            {
              Id = renting.Id,
              IdCar = renting.IdCar,
              IdCustomer = renting.IdCustomer,
              RentalTime = renting.RentalTime,
              ReturnTime = renting.ReturnTime,
              Price = renting.Price,
              Available = renting.Available

            };
        }
        public static List<CustomerRenting> FromRentingList(List<Renting> rentings)
        {
            return rentings.Select(renting => FromRenting(renting)).ToList();
        }

    }
}







