﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Zipcode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Phonenumber { get; set; }

        [DisplayName("Dishes")]
        public List<CartItem> OrderCartItems { get; set; }

        public bool IsDelivered { get; set; }
        public bool IsPayed { get; set; }

        //public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
