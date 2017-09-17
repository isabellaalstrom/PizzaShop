using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaShop.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Guid CartId { get; set; }
        [DisplayName("Dish")]
        public string CartItemName { get; set; }
        public Dish Dish { get; set; }
        public int? DishId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
        public int? OrderId { get; set; }
        public Order Order { get; set; }
        public bool IsModified { get; set; }
    }
}