using Dal.Api;
using Dal.models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dal.Services
{
    public class CustomerService : ICustomerServise, IWorkerService, IUser, IRoleService
    {

        List<Customer> customer = new List<Customer>();
        List<Worker> worker = new List<Worker>();
        public readonly dbClass _context;
        public CustomerService(dbClass context)
        {
            _context = context;
        }

        public bool AddNewCustomer(Customer user)
        {



    
                _context.Customers.Add(user);
                _context.SaveChanges();
                return true;

   

    
        }
    
        
        public bool AddNewCustomer(string id, string firstName, string password, string? lastName, string phoneNumber, string? email, string city, string street, int? buildingNumber)
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
                        Password = password,
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
                return _context.Customers.Include(c => c.IdNavigation).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customers: {ex.Message}");
                return new List<Customer>();
            }
        }

        public List<string> GetAllIdCustomer()
        {
            try
            {
                return _context.Customers.Select(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer IDs: {ex.Message}");
                return new List<string>();
            }
        }


        public List<Renting> GetAllRenting(string id)
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
        public Customer GetCustomerById(string id)
        {
            try
            {
                return _context.Customers
               .Include(c => c.IdNavigation)
               .FirstOrDefault(c => c.Id == id);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving customer by ID {id}: {ex.Message}");
                return null;
            }
        }

        public bool IsExist(string id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }

        public List<string> GetAllIdWorker()
        {
            try
            {
                return _context.Workers.Select(c => c.Id).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Worker IDs: {ex.Message}");
                return new List<string>();
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

        public bool WokerIsExist(string id)
        {
            return _context.Workers.Any(c => c.Id == id);
        }

        public bool AddNewCustomer(User user)
        {

            try
            {
                Customer newCustomer = new Customer
                {
                    Id = user.Id,
                   
                    IdNavigation = new User
                    {
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhonNumber = user.PhonNumber,
                        Email = user.Email,
                        City = user.City,
                        Street = user.Street,
                        BuildingNumber = user.BuildingNumber

                    }
                };


                _context.Customers.Add(newCustomer);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Worker: {ex.Message}");
                Console.WriteLine($"Error adding Worker: {ex.Message}");
                return false;
            }
        }

        public bool AddNewWorker(User user, int hoursMonth, int roleId)
        {

            try
            {
                Worker newWorker = new Worker
                {
                    Id = user.Id,
                    RolsId = roleId,
                    HoursMonth = hoursMonth,
                    IdNavigation = new User
                    {
                        Password = user.Password,
                        FirstName = user.Password,
                        LastName = user.LastName,
                        PhonNumber = user.PhonNumber,
                        Email = user.Email,
                        City = user.City,
                        Street = user.Street,
                        BuildingNumber = user.BuildingNumber

                    }
                };


                _context.Workers.Add(newWorker);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding Worker: {ex.Message}");
                Console.WriteLine($"Error adding Worker: {ex.Message}");
                return false;
            }
        }

        public Customer UpdateCustomer(Customer customer, string id)
        {


            // User _customer = GetUser(id);
            Customer _customer = GetCustomerById(id);
            if (_customer == null)
            {
                return null; 
            }

            
            _customer.IdNavigation.LastName = customer.IdNavigation.LastName;
            _customer.IdNavigation.FirstName = customer.IdNavigation.FirstName;
            _customer.IdNavigation.PhonNumber = customer.IdNavigation.PhonNumber;
            _customer.IdNavigation.Email = customer.IdNavigation.Email;
            _customer.IdNavigation.Password = customer.IdNavigation.Password;
            _customer.IdNavigation.City = customer.IdNavigation.City;
            _customer.IdNavigation.Street = customer.IdNavigation.Street;
            _customer.IdNavigation.BuildingNumber = customer.IdNavigation.BuildingNumber;



            _context.SaveChanges();
            return _customer;
            
            //return _customer; 
        }



    public bool AddNewWorker(string id, string firstName, string? lastName, string password, string phoneNumber, string? email, string city, string street, int? buildingNumber, int hoursMonth, int roleId)
    {

        try
        {
            Worker newWorker = new Worker
            {
                Id = id,
                RolsId = roleId,
                HoursMonth = hoursMonth,
                IdNavigation = new User
                {
                    Password = password,
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

    public Worker GetWorkerById(string id)
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

    public double GetWorkerPrice(string id)
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

    public User GetUser(string id)
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

    public bool IsUserExist(string id)
    {
        return _context.Users.Any(u => u.Id == id);
    }

     
    }

}
