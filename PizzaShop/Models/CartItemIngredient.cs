using System.ComponentModel;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class CartItemIngredient
    {
        public int CartItemIngredientId { get; set; }
        public int CartItemId { get; set; }
        public CartItem CartItem { get; set; }
        [DisplayName("Ingredient")]
        public string IngredientName { get; set; }
        public int Price { get; set; }
    }
}