using Bl.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;
using Dal.models;

namespace Bl.Services
{

    public class SighIn : ISighIn
    {
        ICustomerServise _icustomerServise;
        IWorkerService _workerService;
        IUser _user;
        public SighIn(ICustomerServise icustomerServise, IWorkerService workerService, IUser user) {
            _icustomerServise= icustomerServise;
            _workerService= workerService;
            _user= user;
        }
        public bool IsCustomer(int id)
        {
           return _icustomerServise.IsExist(id);
        }

        public bool IsWorker(int id)
        {
            return _workerService.WokerIsExist(id);
        }

        public User Log(int id, string password)
        {
            if(_user.GetUser(id)==null)
                throw new ArgumentException($"User with id: {id} is not exist");
            if (_user.GetUser(id).Password != password)
                throw new ArgumentException("the password is not correct");
            return _user.GetUser(id);
        }


    }
}
