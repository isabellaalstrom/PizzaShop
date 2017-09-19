using System.ComponentModel;

namespace PizzaShop.Entities
{
    public class CartItemIngredient
    {
        public int CartItemIngredientId { get; set; }
        public int CartItemId { get; set; }
        public CartItem CartItem { get; set; }
        [DisplayName("Ingredient")]
        public string IngredientName { get; set; }
        public int Price { get; set; }
        public bool IsOriginalIngredient { get; set; }
    }
}