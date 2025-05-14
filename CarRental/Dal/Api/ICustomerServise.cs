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
        List<string> GetAllIdCustomer();
        List<Customer> GetAllCustomer();
        bool IsExist(string id);
        bool AddNewCustomer(string id, string firstName, string password, string? lastName, string phoneNumber, string? email, string city, string street, int? buildingNumber);

        bool AddNewCustomer(Customer user);
        bool AddNewCustomer(User user);
        Customer GetCustomerById(string id);
        List<Renting> GetAllRenting(string id);
        Customer UpdateCustomer(Customer customerDto, string id);

    }
}
