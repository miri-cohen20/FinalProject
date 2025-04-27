using Dal.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Api
{
    internal interface IRoleService
    {
        int GetRoleId(string Description);
        string GetRoleDescription(int id);
        double GetRolePrice(int id);
        ICollection<Worker> GetWorkerOnTheRole(int id);
        List<Role>GetAllRoles();
        List<int> GetAllRolesId();




    }
}
