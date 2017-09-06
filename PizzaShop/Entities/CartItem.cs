using System.Collections.Generic;

namespace PizzaShop.Entities
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        //public Cart Cart { get; set; }
        public int CartId { get; set; }
        public Dish Dish { get; set; }
        public int DishId { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public bool IsModified { get; set; }
    }
}