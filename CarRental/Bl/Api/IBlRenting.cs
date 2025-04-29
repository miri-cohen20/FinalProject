using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.models;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace Bl.Api
{
    internal interface IBlRenting
    {
        List<Renting> GetAllMyRenting(int id);
        List<Car> CarAvailableInCertainTime(DateTime fromTime);
        List<Car> CarAvailableInCertainTime(DateTime fromTime, DateTime toTime);
        List<Car> CarAvailableInCertainSeats(int numberOfSeats);
        List<Car> CarAvailableInCertainPlace(string city);
        List<Car> CarAvailableInCertainPlace(string city, string street);

        bool RentingCar(int idCar, int idCustomer, DateTime fromTime, DateTime toTime);
        double GetPriceForRenting(int idCar, DateTime fromTime, DateTime toTime);

        List<Renting> GetAllCurrentRentals(int idCustomer);
        DateTime GetUntilCanRental(int idCustomer, int idRenting);
        bool GetIfCanRentalUntilCertainTime(int idRenting, DateTime untilTime);

        void LackOfCleanliness(int idRenting, string descreption);
        void Improperty(int idRenting, string descreption);

    }
}
//public int Id { get; set; }

//public int IdCar { get; set; }

//public int IdCustomer { get; set; }

//public DateTime RentalTime { get; set; }

//public DateTime ReturnTime { get; set; }

//public double Price { get; set; }