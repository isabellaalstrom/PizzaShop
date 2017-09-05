﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using PizzaShop.Models;

namespace PizzaShop.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [DisplayName("Order placed")]
        public DateTime OrderDateTime { get; set; }
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

        public bool Delivered { get; set; }
    }
}
