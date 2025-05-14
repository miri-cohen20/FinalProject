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
        List<string> GetAllIdWorker();
        List<Worker> GetAllWorker();
        bool WokerIsExist(string id);
        
        bool AddNewWorker(string id, string firstName, string? lastName, string password, string phoneNumber, string? email, string city, string street, int? buildingNumber, int hoursMonth, int roleId);
        bool AddNewWorker(User user, int hoursMonth, int roleId);
        Worker GetWorkerById(string id);
        double GetWorkerPrice(string id);  
        


    



     

      

        
    }
}
