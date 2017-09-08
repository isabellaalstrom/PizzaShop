﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class CheckoutViewModel
    {
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

        public List<CartItem> OrderCartItems { get; set; }
    }
}