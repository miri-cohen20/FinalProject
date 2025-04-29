using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface ICarService
    {
        Car GetCar(int id);
        int GetCarSeats(int id);
        bool IsClean(int id);
       DateTime GetLastCleaning(int id);
        bool IsProper(int id);
        DateTime GetLastCorrection(int id);
        ICollection<Renting> rentings(int id);
        List<Car> GetAllCar();
        List<int> GetAllIdCar();



    }

}
