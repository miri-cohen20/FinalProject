using Bl.Api;
using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;

namespace Bl.Services
{
    internal class BlRenting : IBlRenting
    {
        IRenting _renting;
        IPrice _price;
        ITime _time;
        ICarService _carService;
        public BlRenting(IRenting irenting, ICarService carService, IPrice price, ITime time)
        {
            _renting = irenting;
            _carService = carService;
            _price = price;
            _time = time;
        }

        public List<Car> CarAvailableInCertainPlace(string city)
        {

            return _carService.GetAllCar().FindAll(x => x.City == city);
        }

        public List<Car> CarAvailableInCertainPlace(string city, string street)
        {
            return CarAvailableInCertainPlace(city).FindAll(x => x.Street == street);
        }

        public List<Car> CarAvailableInCertainSeats(int numberOfSeats)
        {
            return _carService.GetAllCar().FindAll(c => c.Seats == numberOfSeats);
        }

        public List<Car> CarAvailableInCertainTime(DateTime fromTime)
        {
            var unavailableCarIds = _renting.GetAllRenting()
                .Where(r => fromTime >= r.RentalTime && fromTime <= r.ReturnTime)
                .Select(r => r.IdCar)
                .Distinct()
                .ToList();

            var availableCars = _carService.GetAllCar()
                .Where(car => !unavailableCarIds.Contains(car.Id))
                .ToList();

            return availableCars;
        }

        public List<Car> CarAvailableInCertainTime(DateTime fromTime, DateTime toTime)
        {
            var unavailableCarIds = _renting.GetAllRenting()
                .Where(r => (fromTime < r.ReturnTime && toTime > r.RentalTime))
                .Select(r => r.IdCar)
                .Distinct()
                .ToList();

            var availableCars = _carService.GetAllCar()
                .Where(car => !unavailableCarIds.Contains(car.Id))
                .ToList();

            return availableCars;
        }


        public List<Renting> GetAllCurrentRentals(int idCustomer)
        {
            DateTime currentTime = DateTime.Now;

            var currentRentals = _renting.GetAllRenting()
                .Where(r => r.IdCustomer == idCustomer && r.RentalTime <= currentTime && r.ReturnTime >= currentTime)
                .ToList();

            return currentRentals;
        }

        public List<Renting> GetAllMyRenting(int id)
        {
            return _renting.GetAllRenting()
                .Where(r=>r.IdCustomer == id)
                .ToList();

        }

        public bool ExtendingRentalForACertainPeriodTime (int idRenting, int customerId, DateTime untilTime)
        {
            DateTime currentTime = DateTime.Now;

            // בודק אם ההשכרה קיימת והלקוח הוא זה שמשכיר אותה
            var rental = _renting.GetAllRenting()
                .FirstOrDefault(r => r.Id == idRenting && r.IdCustomer == customerId);

            if (rental == null|| rental.RentalTime < currentTime)
            {
                if(rental == null)
                   // Console.WriteLine("is not exist a rental ");
                return false;
            }

            if( CarAvailableInCertainTime(rental.ReturnTime, untilTime).Find(c=>c.Id==rental.Id)!=null)
            {
              return  _renting.UpdateReturnTimeRenting(idRenting, untilTime);
              
            }
            return false;
             
        }

        public bool GetIfCanRentalUntilCertainTime(int idRenting, DateTime untilTime)
        {
            throw new NotImplementedException();
        }

        public double GetPriceForRenting(int idCar, DateTime fromTime, DateTime toTime)
        {
            double totalHours = (toTime - fromTime).TotalHours;

            if (totalHours <= 0)
            {
                throw new ArgumentException("תאריך ההחזרה צריך להיות לאחר תאריך ההשכרה.");
            }

            // כאן נניח שיש לנו דרך להשיג את מספר המושבים של הרכב על פי ה-ID שלו
            int seats = _price.GetSeatsById(idCar);
            double priceForHours;

            if (totalHours<24)
            {
                 priceForHours = _price.GetPriceForHour(seats, 1);
            }
           else if (totalHours < 144)
            {
                 priceForHours = _price.GetPriceForHour(seats, 2);
            }
            else
            {
                 priceForHours = _price.GetPriceForHour(seats, 3);
            }
            
            return priceForHours * totalHours;
        }

        public DateTime? UntilWhenCanACertainCarBeRented(int idCar, DateTime from)
        {
            var carExists = _carService.GetAllCar().Any(car => car.Id == idCar);
            if (!carExists)
            {
                throw new ArgumentException("Car with the specified ID does not exist.");
            }

            var conflictingRentals= _renting.GetAllRenting()
                .Where(r=>r.IdCar == idCar &&
                r.RentalTime<from &&
                r.ReturnTime>from)
                .ToList(); 
            if(conflictingRentals.Count==0)
            {

                return MaxRenting(from);
            }

            return null;
        }






        public DateTime MaxRenting(DateTime inputDate)
        {
            return inputDate.AddMonths(1);
        }

        public DateTime GetUntilCanRental(int idCustomer, int idRenting)
        {
            throw new NotImplementedException();
        }

        public void Improperty(int idRenting, string descreption)
        {
            throw new NotImplementedException();
        }

        public void LackOfCleanliness(int idRenting, string descreption)
        {
            throw new NotImplementedException();
        }

        public bool RentingCar(int idCar, int idCustomer, DateTime fromTime, DateTime toTime)
        {
            throw new NotImplementedException();
        }


    }
}
