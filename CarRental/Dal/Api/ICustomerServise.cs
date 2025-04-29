using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal.models;

namespace Dal.Api
{
    public interface ICustomerServise
    {
        List<int> GetAllIdCustomer();
        List<Customer> GetAllCustomer();
        bool IsExist(int id);
        bool AddNewCustomer(int id, string firstName, string? lastNamer, int phoneNumber, string? email, string city, string street, int? buildingNumber);
        Customer GetCustomerById(int id);
        Renting GetAllRenting(int id);

    }
}
