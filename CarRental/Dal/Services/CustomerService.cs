using Dal.Api;
using Dal.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dal.Services
{
    public class CustomerService : ICustomerServise, IWorkerService, IUser,IRoleService
    {

        List<Customer> customer = new List<Customer>();
        List<Worker> worker = new List<Worker>();
        public readonly dbClass _context;
        public CustomerService(dbClass context)
        {
            _context = context;
        }


        public bool AddNewCustomer(int id, string firstName, string? lastName, int phoneNumber, string? email, string city, string street, int? buildingNumber)
        {

            if (IsExist(id))
            {
                return false;
            }

            try
            {
                Customer newCustomer = new Customer
                {
                    Id = id,
                    IdNavigation = new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhonNumber = phoneNumber,
                        Email = email,
                        City = city,
                        Street = street,
                        BuildingNumber = buildingNumber
                    }
                };


                _context.Customers.Add(newCustomer);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding customer: {ex.Message}");
                return false;
            }
        }

        public List<Customer> GetAllCustomer()
        {
            try
            {
                return _context.Customers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customers: {ex.Message}");
                return new List<Customer>();
            }
        }

        public List<int> GetAllIdCustomer()
        {
            try
            {
                return _context.Customers.Select(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer IDs: {ex.Message}");
                return new List<int>();
            }
        }


        public List<Renting> GetAllRenting(int id)
        {

            try
            {

                var customer = _context.Customers.Find(id);

                return customer?.Rentings.ToList() ?? new List<Renting>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving renting for customer ID {id}: {ex.Message}");

                return new List<Renting>();
            }
        }
        public Customer GetCustomerById(int id)
        {
            try
            {
                return _context.Customers.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer by ID {id}: {ex.Message}");
                return null;
            }
        }

        public bool IsExist(int id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }

        public List<int> GetAllIdWorker()
        {
            try
            {
                return _context.Workers.Select(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Worker IDs: {ex.Message}");
                return new List<int>();
            }
        }

        public List<Worker> GetAllWorker()
        {
            try
            {
                return _context.Workers.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Worker: {ex.Message}");
                return new List<Worker>();
            }
        }

        public bool WokerIsExist(int id)
        {
            return _context.Workers.Any(c => c.Id == id);
        }

        public bool AddNewWoker(int id, string firstName, string? lastName, int phoneNumber, string? email, string city, string street, int? buildingNumber, int hoursMonth, int roleId)
        {

            if (IsExist(id))
            {
                return false;
            }

            try
            {
                Worker newWorker = new Worker
                {
                    Id = id,
                    IdNavigation = new User
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhonNumber = phoneNumber,
                        Email = email,
                        City = city,
                        Street = street,
                        BuildingNumber = buildingNumber
                    }
                };


                _context.Workers.Add(newWorker);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Worker: {ex.Message}");
                return false;
            }

        }

        public Worker GetWorkerById(int id)
        {
            try
            {
                return _context.Workers.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Worker by ID {id}: {ex.Message}");
                return null;
            }
        }

        public double GetWorkerPrice(int id)
        {
            try
            {
                var worker = _context.Workers.Find(id);
                if (worker != null)
                {
                    return worker.Price;
                }
                else
                {
                    throw new Exception("Worker not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving worker price for ID {id}: {ex.Message}");
                throw;
                {
                }
            }
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public int GetRoleId(string description)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Description.Equals(description, StringComparison.OrdinalIgnoreCase));
            return role?.Id ?? -1; 
        }

        public string GetRoleDescription(int id)
        {
            var role = _context.Roles.Find(id);
            return role?.Description ?? string.Empty; 
        }

        public double GetRolePrice(int id)
        {
            var role = _context.Roles.Find(id);
            return role?.PriceHour ?? 0; 
        }

        public ICollection<Worker> GetWorkerOnTheRole(int id)
        {
            var role = _context.Roles.Include(r => r.Workers).FirstOrDefault(r => r.Id == id);
            return role?.Workers ?? new List<Worker>(); 
        }

        public List<Role> GetAllRoles()
        {
            try
            {
                return _context.Roles.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving roles: {ex.Message}");
                return new List<Role>(); 
            }
        }

        public List<int> GetAllRolesId()
        {
            try
            {
                return _context.Roles.Select(r => r.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving role IDs: {ex.Message}");
                return new List<int>(); 
            }
        }
    }

}
