using System.Collections.Generic;
using System.ComponentModel;

namespace PizzaShop.Entities
{
    public class Dish
    {
        public int DishId { get; set; }
        [DisplayName("Dish")]
        public string DishName { get; set; }
        public int Price { get; set; }
        public int DishTypeId { get; set; }
        public DishType DishType { get; set; }
        [DisplayName("Ingredients")]
        public List<DishIngredient> DishIngredients { get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
