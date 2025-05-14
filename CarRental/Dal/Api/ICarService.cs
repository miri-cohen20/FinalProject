using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICarService
    {
        
        void ComplaintOfCleanliness(int idRenting, string descreption);
        void ComplaintOfImproperty(int idRenting, string descreption);
        Car GetCar(string id);
        int GetCarSeats(string id);
        bool IsClean(string id);
        DateTime GetLastCleaning(string id);
        bool IsProper(string id);
        DateTime GetLastCorrection(string id);
        ICollection<Renting> rentings(string id);
        List<Car> GetAllCar();
        List<string> GetAllIdCar();



    }

}
