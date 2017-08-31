﻿using System.Collections.Generic;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Cart Cart { get; set; }
        public int CartId { get; set; }
        public Dish Dish { get; set; }
        public int DishId { get; set; }
        public int Price { get; set; }
        //public int Quantity { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
    }
}