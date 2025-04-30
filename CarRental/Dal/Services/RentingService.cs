using Dal.Api;
using Dal.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Services
{
    internal class RentingService : IPrice, ITime, IRenting, ICarService
    {
        public readonly dbClass _context;
        public RentingService(dbClass context)
        {
            _context = context;
        }

        public double GetPriceForHour(int seats, int time)
        {
            var priceEntry = _context.Prices
                .FirstOrDefault(p => p.Seats == seats && p.Time == time);
            return priceEntry?.PriceForHour ?? 0;
        }

        public int GetIdPrice(int seats, int time)
        {
            var priceEntry = _context.Prices
                .FirstOrDefault(p => p.Seats == seats && p.Time == time);
            return priceEntry?.Id ?? -1;
        }

        public double GetPriceForHourById(int id)
        {
            var priceEntry = _context.Prices.Find(id);
            return priceEntry?.PriceForHour ?? 0;
        }

        public int GetSeatsById(int id)
        {
            var priceEntry = _context.Prices.Find(id);
            return priceEntry?.Seats ?? -1;
        }

        public int GetTimeById(int id)
        {
            var priceEntry = _context.Prices.Find(id);
            return priceEntry?.Time ?? -1;
        }

        public List<Price> GetAllPrices()
        {
            try
            {
                return _context.Prices.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving prices: {ex.Message}");
                return new List<Price>();
            }
        }

        public List<int> GetAllIdPrice()
        {
            try
            {
                return _context.Prices.Select(p => p.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving price IDs: {ex.Message}");
                return new List<int>();
            }
        }

        public int GetIdOfDuration(string duration)
        {
            var timeEntry = _context.Times
                .FirstOrDefault(t => t.Duration.Equals(duration, StringComparison.OrdinalIgnoreCase));
            return timeEntry?.Id ?? -1;
        }

        public string GetIdDuration(int id)
        {
            var timeEntry = _context.Times.Find(id);
            return timeEntry?.Duration ?? string.Empty;
        }

        public List<string> GetAllDuration()
        {
            try
            {
                return _context.Times.Select(t => t.Duration).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving durations: {ex.Message}");
                return new List<string>();
            }
        }

        public List<Time> GetAllTime()
        {
            try
            {
                return _context.Times.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving times: {ex.Message}");
                return new List<Time>();
            }
        }

        public List<Renting> GetAllRenting()
        {
            try
            {
                return _context.Rentings.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving rentings: {ex.Message}");
                return new List<Renting>();
            }
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Find(id);
        }

        public int GetCarSeats(int id)
        {
            var car = _context.Cars.Find(id);
            return car?.Seats ?? -1;
        }

        public bool IsClean(int id)
        {
            var car = _context.Cars.Find(id);
            return car?.CleanStatus ?? false;
        }

        public DateTime GetLastCleaning(int id)
        {
            var car = _context.Cars.Find(id);
            return car?.FinalCleaning ?? DateTime.MinValue;
        }

        public bool IsProper(int id)
        {
            var car = _context.Cars.Find(id);
            return car?.ProperStatus ?? false;
        }

        public DateTime GetLastCorrection(int id)
        {
            var car = _context.Cars.Find(id);
            return car?.LastCorrection ?? DateTime.MinValue;
        }

        public ICollection<Renting> rentings(int id)
        {
            var car = _context.Cars.Include(c => c.Rentings).FirstOrDefault(c => c.Id == id);
            return car?.Rentings ?? new List<Renting>();
        }

        public List<Car> GetAllCar()
        {
            return _context.Cars.ToList();
        }

        public List<int> GetAllIdCar()
        {
            return _context.Cars.Select(c => c.Id).ToList();
        }

        public bool UpdateReturnTimeRenting(int idRenting, DateTime returnTime)
        {
            var renting = _context.Rentings.FirstOrDefault(r => r.Id == idRenting);
            if (renting == null)
                throw new ArgumentException("Renting with the specified ID does not exist.");

            renting.ReturnTime = returnTime; 

            _context.SaveChanges(); 
            return true; 
        }

        public bool AddRenting(int idCar, int idCustomer, DateTime rentalTime, DateTime returnTime, double price)
        {

            try
            {
                Renting newRenting = new Renting
                {

                    Id = idCar,
                    IdCustomer = idCustomer,
                    RentalTime = rentalTime,
                    ReturnTime = returnTime,
                    Price = price
                };


                _context.Rentings.Add(newRenting);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Renting: {ex.Message}");
                return false;
            }
        }
    }
}

