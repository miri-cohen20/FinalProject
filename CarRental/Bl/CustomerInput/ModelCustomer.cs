using Dal.models;
using System.Collections.Generic;
using YourNamespace;

namespace YourNamespace // Use the appropriate namespace
{
    public class CustomerRegistration
    {
        // Properties required for registration
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;
        public int? BuildingNumber { get; set; }
        public string Password { get; set; } = string.Empty;

        // Method to create and return a Customer object
        public Customer CreateCustomer()
        {

            return new Customer
            {
                Id = this.Id,
                IdNavigation = new User
                {
                    Id = this.Id,
                    FirstName = this.FirstName,
                    LastName = this.LastName,
                    PhonNumber = this.PhoneNumber,
                    Email = this.Email,
                    City = this.City,
                    Street = this.Street,
                    BuildingNumber = this.BuildingNumber,
                    Password = this.Password,
                },
                Rentings = new List<Renting>() 
            };
        }
        public static CustomerRegistration FromCustomer(Customer customer)
        {
            return new CustomerRegistration
            {
                Id = customer.Id,
                FirstName = customer.IdNavigation.FirstName,
                LastName = customer.IdNavigation.LastName,
                PhoneNumber = customer.IdNavigation.PhonNumber,
                Email = customer.IdNavigation.Email,
                City = customer.IdNavigation.City,
                Street = customer.IdNavigation.Street,
                BuildingNumber = customer.IdNavigation.BuildingNumber,
                Password = customer.IdNavigation.Password // אם יש צורך, ניתן לשקול לא להחזיר סיסמה
            };
        }
    }
}





//Customer customer = new Customer
//{
//    Id = 1, // או כל ID תקין אחר
//    IdNavigation = new User
//    {
//        Id = 1,
//        FirstName = "John",
//        LastName = "Doe",
//        PhonNumber = "123456789",
//        Email = "john.doe@example.com",
//        City = "New York",
//        Street = "5th Avenue",
//        BuildingNumber = 10,
//        Password = "securepassword"
//    },
//    Rentings = new List<Renting>
//    {
//        new Renting { /* אתחול פרמטרים של Renting כאן */ },
//        new Renting { /* אתחול נוסף אם יש צורך */ }
//    }
//};
