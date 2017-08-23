using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models
{
    public class Dish
    {
        public int DishId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        [DisplayName("Ingredients")]
        public List<DishIngredient> DishIngredients { get; set; }
        public List<OrderDish> OrderDishes { get; set; }
    }
}
