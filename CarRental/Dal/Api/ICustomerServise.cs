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
        bool AddNewCustomer(int id, string firstName, string password, string? lastName, string phoneNumber, string? email, string city, string street, int? buildingNumber);

        bool AddNewCustomer(Customer user);
        bool AddNewCustomer(User user);
        Customer GetCustomerById(int id);
        List<Renting> GetAllRenting(int id);
        bool UpdateCustomer(Customer customerDto);

    }
}
