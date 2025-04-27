using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Api
{
    internal interface ILogIn
    {

        bool Log(int id, string password);
        bool IsCustomer(int id);
        bool IsWorker(int id);

    }
}
