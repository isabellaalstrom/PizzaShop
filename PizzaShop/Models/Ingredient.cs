using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        [DisplayName("Ingredient")]
        public string IngredientName { get; set; }
        public List<DishIngredient> DishIngredients { get; set; }
        public List<CartItemIngredient> CartItemIngredients { get; set; }
    }
}
