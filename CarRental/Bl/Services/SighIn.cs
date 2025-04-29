using Bl.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.Api;

namespace Bl.Services
{

    internal class SighIn : ISighIn
    {
        ICustomerServise _icustomerServise;
        IWorkerService _workerService;
        public SighIn(ICustomerServise icustomerServise, IWorkerService workerService) {
            _icustomerServise= icustomerServise;
            _workerService= workerService;
        }
        public bool IsCustomer(int id)
        {
           return _icustomerServise.IsExist(id);
        }

        public bool IsWorker(int id)
        {
            return _workerService.WokerIsExist(id);
        }

        public bool Log(int id, string password)
        {
            throw new NotImplementedException();
        }
    }
}
