using Dal.models;
using System.Collections.Generic;

namespace YourNamespace // Use the appropriate namespace
{
    public class CustomerRegistration
    {
        // Properties required for registration
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
            var user = new User
            {
                FirstName = this.FirstName,
                LastName = this.LastName,
                PhonNumber = this.PhoneNumber,
                Email = this.Email,
                City = this.City,
                Street = this.Street,
                BuildingNumber = this.BuildingNumber,
                Password = this.Password,
            };

            return new Customer
            {
                IdNavigation = user,
                Rentings = new List<Renting>() // Initialize Rentings as an empty list
            };
        }
    }
}
