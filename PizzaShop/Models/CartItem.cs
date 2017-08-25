﻿using System.Collections.Generic;

namespace PizzaShop.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Cart Cart { get; set; }
        public string CartId { get; set; }
        public Dish Dish { get; set; }
        public int DishId { get; set; }
        public int Quantity { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
    }
}