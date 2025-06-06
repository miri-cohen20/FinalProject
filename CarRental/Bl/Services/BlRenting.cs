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
using Dal.Services;


namespace Bl.Services
{
    public class BlRenting : IBlRenting
    {
        IRenting _renting;
        IPrice _price;
        ITime _time;
        ICarService _carService;
        ICustomerServise _customerServise;
        public BlRenting(IRenting irenting, ICarService carService, IPrice price, ITime time, ICustomerServise customerServise)
        {
            _renting = irenting;
            _carService = carService;
            _price = price;
            _time = time;
            _customerServise = customerServise;
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


        public List<Renting> GetAllMyCurrentRentals(string idCustomer)
        {
            if (!_customerServise.IsExist(idCustomer))
            {
                throw new ArgumentException("Customer with the specified ID does not exist.");
            }
            DateTime currentTime = DateTime.Now;

            var currentRentals = _renting.GetAllRenting()
                .Where(r => r.IdCustomer == idCustomer && r.RentalTime <= currentTime && r.ReturnTime >= currentTime)
                .ToList();

            return currentRentals;
        }

        public List<Renting> GetAllMyRenting(string id)
        {
            if(!_customerServise.IsExist(id))
            {
                throw new ArgumentException("Customer with the specified ID does not exist.");
            }
            return _renting.GetAllRenting()
                .Where(r => r.IdCustomer == id)
                .ToList();

        }

        public double PriceAddedRentalExtension(int idRenting, string customerId, DateTime untilTime)
        {

            if (!IsRentingActiveOrFuture(customerId, idRenting))
                throw new InvalidOperationException("The customer is not renting this vehicle now or in future.");


            // בודק אם ההשכרה קיימת והלקוח הוא זה שמשכיר אותה
            var rental = _renting.GetAllRenting()
                .FirstOrDefault(r => r.Id == idRenting && r.IdCustomer == customerId);

            if (rental == null)
            {

                throw new ArgumentException($"This rental was not found for the customer with the same number: {customerId}");

            }

            if (CarAvailableInCertainTime(rental.ReturnTime, untilTime).Find(c => c.Id == rental.IdCar) != null)
            {
               return GetPriceForRenting(rental.IdCar, rental.RentalTime, untilTime) - rental.Price;
                

            }
            throw new InvalidOperationException("The rental cannot be extended until the requested time.");



        }


        public bool ExtendingRentalForACertainPeriodTime(int idRenting, string customerId, DateTime untilTime)
        {
            
            if(!_customerServise.IsExist(customerId))
            {
                throw new ArgumentException("Customer with the specified ID does not exist.");
            }
            if(!IsRentingActiveOrFuture( customerId, idRenting))
                throw new InvalidOperationException("The customer is not renting this vehicle now or in future.");
            

            // בודק אם ההשכרה קיימת והלקוח הוא זה שמשכיר אותה
            var rental = _renting.GetAllRenting()
                .FirstOrDefault(r => r.Id == idRenting && r.IdCustomer == customerId);

            if (rental == null )
            {
                
                    throw new ArgumentException($"This rental was not found for the customer with the same number: {customerId}");

            }

            if (CarAvailableInCertainTime(rental.ReturnTime, untilTime).Find(c => c.Id == rental.IdCar) != null)
            {
                double price = GetPriceForRenting(rental.IdCar, rental.RentalTime, untilTime);
                return _renting.UpdateReturnTimeRenting(idRenting, untilTime,  price);

            }
            return false;

        }

        public bool GetIfCanRentalUntilCertainTime(int idRenting, DateTime untilTime)
        {
            // בדוק אם ההשכרה קיימת
            var renting = _renting.GetAllRenting().FirstOrDefault(r => r.Id == idRenting);
            if (renting == null)
            {
                throw new ArgumentException("Renting with the specified ID does not exist.");
            }

            // בדוק אם untilTime הוא אחרי תאריך הסיום של ההשכרה
            if (untilTime < renting.ReturnTime)
                return false;
            return UntilWhenCanACertainCarBeRented(renting.IdCar, renting.ReturnTime) != null && UntilWhenCanACertainCarBeRented(renting.IdCar, renting.ReturnTime) >= untilTime;

        }



        public double GetPriceForRenting(string idCar, DateTime fromTime, DateTime toTime)
        {
            double totalHours = (toTime - fromTime).TotalHours;

            if (totalHours <= 0)
            {
                throw new ArgumentException("תאריך ההחזרה צריך להיות לאחר תאריך ההשכרה.");
            }

            // כאן נניח שיש לנו דרך להשיג את מספר המושבים של הרכב על פי ה-ID שלו
            int seats = _price.GetSeatsById(idCar);
            double priceForHours;

            if (totalHours < 24)
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

        public DateTime? UntilWhenCanACertainCarBeRented(string idCar, DateTime from)
        {
            var carExists = _carService.GetAllCar().Any(car => car.Id == idCar);
            if (!carExists)
            {
                throw new ArgumentException("Car with the specified ID does not exist.");
            }

            var conflictingRentals = _renting.GetAllRenting()
                .Where(r => r.IdCar == idCar &&
                r.RentalTime >= from)
                .ToList();
            

            if (conflictingRentals.Count == 0)
            {

                return MaxRenting(from);
            }
            DateTime lastRentalTime = conflictingRentals.Min(r => r.RentalTime);
            if (lastRentalTime>MaxRenting(from))
            {
                return MaxRenting(from);
            }
            else return lastRentalTime;    
        }


        public DateTime MaxRenting(DateTime inputDate)
        {
            return inputDate.AddMonths(1);
        }





        public void Improperty(int idRenting, string descreption)
        {

            var renting = _renting.GetAllRenting().FirstOrDefault(r => r.Id == idRenting);
            if (renting == null)
            {
                throw new ArgumentException("Renting with the specified ID does not exist.");
            }




            if (IsRentingActive(idRenting))
            {


                _carService.ComplaintOfImproperty(idRenting, descreption);

            }
            else
            {
                throw new InvalidOperationException("The renting is either inactive or has ended.");
            }
        }

        public void LackOfCleanliness(int idRenting, string descreption)
        {

            var renting = _renting.GetAllRenting().FirstOrDefault(r => r.Id == idRenting);
            if (renting == null)
            {
                throw new ArgumentException("Renting with the specified ID does not exist.");
            }


            

            if (IsRentingActive(idRenting))
            {

               
                _carService.ComplaintOfCleanliness(idRenting, descreption);
               
            }
            else
            {
                throw new InvalidOperationException("The renting is either inactive or has ended.");
            }
        }

        public void EndRental(string idCustomer, int idRenting)
        {
            if(!IsRentingActiveByCustomer(idRenting,idCustomer))
                throw new InvalidOperationException("The customer is not renting this vehicle now.");
            _renting.UpdateEndRental(idRenting);  
        }
        public bool IsRentingActive(int idRenting)
        {
            Renting r = _renting.GetAllRenting().Find(r => r.Id == idRenting);
            if (r == null)
                throw new ArgumentException("Renting with the specified ID does not exist.");

            
            DateTime now = DateTime.Now;
            return r.RentalTime <= now && (r.ReturnTime == null || r.ReturnTime > now);
        }

        public bool IsRentingActiveByCustomer(int idRenting, string idCustomer)
        {
            if (!_customerServise.GetAllCustomer().Any(c => c.Id == idCustomer))
                throw new ArgumentException("Customer with the specified ID does not exist.");
            Renting r = _renting.GetAllRenting().Find(r => r.Id == idRenting);
            if (r == null)
                throw new ArgumentException("Renting with the specified ID does not exist.");
            if (r.IdCustomer != idCustomer)
                throw new ArgumentException("The customer is not renting this renting.");
            DateTime now = DateTime.Now;
            return r.RentalTime <= now && (r.ReturnTime == null || r.ReturnTime > now);
        }

        public bool IsRentingActiveOrFuture(string idCustomer, int idRenting)
        {
            if (!_customerServise.GetAllCustomer().Any(c => c.Id == idCustomer))
                throw new ArgumentException("Customer with the specified ID does not exist.");

            Renting r = _renting.GetAllRenting().Find(r => r.Id == idRenting);
            if (r == null)
                throw new ArgumentException("Renting with the specified ID does not exist.");
            if (r.IdCustomer != idCustomer)
                throw new ArgumentException("The customer is not associated with this renting.");

            DateTime now = DateTime.Now;
            return (r.RentalTime <= now && (r.ReturnTime == null || r.ReturnTime > now)) || (r.RentalTime > now);
        }



        public bool RentingCar(string idCar, string idCustomer, DateTime fromTime, DateTime toTime)
        {
            // בדוק אם קיים רכב עם ה-ID הנתון
            var car = _carService.GetAllCar().FirstOrDefault(c => c.Id == idCar);
            if (car == null)
            {
                throw new ArgumentException("Car with the specified ID does not exist.");
            }

            // בדוק אם קיים לקוח עם ה-ID הנתון
            var customer = _customerServise.GetAllCustomer().FirstOrDefault(c => c.Id == idCustomer);
            if (customer == null)
            {
                throw new ArgumentException("Customer with the specified ID does not exist.");
            }

            // בדוק אם הזמן פנוי להשכרה


            if (!CarAvailableInCertainTime(fromTime, toTime).Any(r => r.Id == idCar))
            {
                throw new InvalidOperationException("The car is already rented during the specified time.");
            }
            if (fromTime.AddMinutes(5) < DateTime.Now)
            {
                throw new InvalidOperationException("Incorrect time");
            }


            _renting.AddRenting(idCar, idCustomer, fromTime, toTime, GetPriceForRenting(idCar, fromTime, toTime));

            return true;
        }

        public DateTime? GetUntilCanRental(string idCustomer, int idRenting)
        {
            if (!_customerServise.GetAllCustomer().Any(c => c.Id == idCustomer))
                throw new ArgumentException("Customer with the specified ID does not exist.");
            if (!_renting.GetAllRenting().Any(_renting => _renting.Id == idRenting))
                throw new ArgumentException("Renting with the specified ID does not exist.");
            return UntilWhenCanACertainCarBeRented(_renting.GetAllRenting().Find(r => r.Id == idRenting).IdCar, _renting.GetAllRenting().Find(r => r.Id == idRenting).ReturnTime);

        }
        public Customer UpdateCustomer(Customer customerDto, string id)
        {
            if (customerDto == null)
                throw new ArgumentNullException();
            if(_customerServise.IsExist(id)!=true)
            {
                throw new InvalidOperationException("No client identified for editing");
            }

                
            Customer user = _customerServise.UpdateCustomer(customerDto,id);
            if (user == null)
                throw new InvalidOperationException("the user with this id is not exsist");
            return user;
             
        }

        public List<Car> GetAllCars()
        {
            return _carService.GetAllCar();
        }

        public List<Renting> GetAllRenting()
        {
          return  _renting.GetAllRenting();
        }


        public List<Renting> GetActiveAndFutureRentalsByCustomer(string idCustomer)
        {

            if (!_customerServise.GetAllCustomer().Any(c => c.Id == idCustomer))
                throw new ArgumentException("Customer with the specified ID does not exist.");

            DateTime now = DateTime.Now;

            var activeAndFutureRentals = _renting.GetAllRenting().Where(r =>
                r.IdCustomer == idCustomer &&
                (r.RentalTime <= now && (r.ReturnTime == null || r.ReturnTime > now) ||
                 r.RentalTime > now)).ToList();

            return activeAndFutureRentals;
        }


    }

}

