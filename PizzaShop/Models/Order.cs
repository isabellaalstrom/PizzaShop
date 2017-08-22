﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [DisplayName("Order placed")]
        public DateTime OrderDateTime { get; set; }
        [DisplayName("Total amount")]
        public int TotalAmount { get; set; } //todo ska räknas ut från orderdishes.dishes priser

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public List<OrderDish> OrderDishes { get; set; }
    }
}
