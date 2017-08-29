using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace PizzaShop.Entities
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }

        public List<Order> Orders { get; set; }
    }
}
