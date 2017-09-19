using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaShop.Entities
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        [DisplayName("Ingredient")]
        public string IngredientName { get; set; }
        public int Price { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
    }
}
