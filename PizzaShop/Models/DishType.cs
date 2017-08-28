using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models
{
    public class DishType
    {
        public int DishTypeId { get; set; }
        public string DishTypeName { get; set; }
        public List<Dish> Dishes { get; set; }
    }
}
