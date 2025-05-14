using Dal.Api;
using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    public interface ISighIn
    {

        User Log(string id, string password);
        bool IsCustomer(string id);
        bool IsWorker(string id);

    }
}
