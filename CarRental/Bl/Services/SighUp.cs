using Bl.Api;
using Dal.Api;
using Dal.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Services
{
    public class SighUp : ISighUp
    {
        ICustomerServise _icustomerServise;
        IWorkerService _workerService;
        IUser _user;
        public SighUp(ICustomerServise icustomerServise, IWorkerService workerService, IUser user)
        {
            _icustomerServise = icustomerServise;
            _workerService = workerService;
            _user = user;
        }


        public bool CreateCustomer(string id, string firstName,string password, string phoneNumber, string city, string street, int buildingNumber = 0, string lastName = null, string email = null)
        {
           if(!_user.IsUserExist(id))
                return false;
            _icustomerServise.AddNewCustomer(id, firstName, password, lastName, phoneNumber,email, city, street,buildingNumber);
            return true;
            
        }

        public bool CreateCustomer(Customer user)
        {
           

            if (_user.IsUserExist(user.IdNavigation.Id))
                return false;
            _icustomerServise.AddNewCustomer(user);
            return true;
        }



        public bool CreateWorker(string id, string firstName,string password, string phoneNumber, string city, string street, int hoursMonth, int roleId, int buildingNumber = 0, string lastName = null, string email = null)
        {
            if(_workerService.WokerIsExist(id))
                return false;
            _workerService.AddNewWorker(id, firstName,  lastName, password, phoneNumber, email, city, street, buildingNumber,hoursMonth, roleId);
            if(!_icustomerServise.IsExist(id))
                _icustomerServise.AddNewCustomer(id, firstName, password, lastName, phoneNumber, email, city, street, buildingNumber);
            return true;
        }

        public bool CreateWorker(string id, int hoursMonth, int roleId)
        {
            if (_workerService.WokerIsExist(id))
                return false;
            _workerService.AddNewWorker(_user.GetUser(id), hoursMonth, roleId);
            if(!_icustomerServise.IsExist(id))
                _icustomerServise.AddNewCustomer(_user.GetUser(id));
            return true;


        }

  
    }
}
