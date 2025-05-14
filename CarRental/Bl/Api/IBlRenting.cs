using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dal.models;

namespace Bl.Api
{
    public interface IBlRenting
    {
        List<Car> GetAllCars();
        List<Renting> GetAllRenting();
        List<Renting> GetAllMyRenting(string id);
        List<Car> CarAvailableInCertainTime(DateTime fromTime);
        List<Car> CarAvailableInCertainTime(DateTime fromTime, DateTime toTime);
        List<Car> CarAvailableInCertainSeats(int numberOfSeats);
        List<Car> CarAvailableInCertainPlace(string city);
        List<Car> CarAvailableInCertainPlace(string city, string street);
        void EndRental(string idCustomer, int idRenting);

        bool RentingCar(string idCar, string idCustomer, DateTime fromTime, DateTime toTime);
        double GetPriceForRenting(string idCar, DateTime fromTime, DateTime toTime);

        List<Renting> GetAllMyCurrentRentals(string idCustomer);
        DateTime? GetUntilCanRental(string idCustomer, int idRenting);
        bool ExtendingRentalForACertainPeriodTime(int idRenting, string customerId, DateTime untilTime);
        double PriceAddedRentalExtension(int idRenting, string customerId, DateTime untilTime);

        void LackOfCleanliness(int idRenting, string descreption);
        void Improperty(int idRenting, string descreption);
        
        DateTime MaxRenting(DateTime inputDate);
        Customer UpdateCustomer(Customer customerDto, string id);
        DateTime? UntilWhenCanACertainCarBeRented(string idCar, DateTime from);

    }
}
//public int Id { get; set; }

//public int IdCar { get; set; }

//public int IdCustomer { get; set; }

//public DateTime RentalTime { get; set; }

//public DateTime ReturnTime { get; set; }

//public double Price { get; set; }