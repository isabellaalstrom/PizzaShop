using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class CreateOrderViewModel
    {
        [DisplayName("Total amount")]
        public int TotalAmount { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Phonenumber { get; set; }

        public List<OrderDish> OrderDishes { get; set; }
    }
}
