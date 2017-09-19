using System;
using System.Collections.Generic;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class Cart
    {
        public Guid CartId { get; set; }
        public List<CartItem> CartItems = new List<CartItem>();
    }
}
