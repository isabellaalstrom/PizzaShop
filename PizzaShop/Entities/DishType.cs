using System.Collections.Generic;

namespace PizzaShop.Entities
{
    public class DishType
    {
        public int DishTypeId { get; set; }
        public string DishTypeName { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
