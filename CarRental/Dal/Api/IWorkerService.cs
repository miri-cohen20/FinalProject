using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    public interface IWorkerService
    {
        List<int> GetAllIdWorker();
        List<Worker> GetAllWorker();
        bool WokerIsExist(int id);
        
        bool AddNewWorker(int id, string firstName, string? lastName, string password, int phoneNumber, string? email, string city, string street, int? buildingNumber, int hoursMonth, int roleId);
        bool AddNewWorker(User user, int hoursMonth, int roleId);
        Worker GetWorkerById(int id);
        double GetWorkerPrice(int id);  
        


    



     

      

        
    }
}
