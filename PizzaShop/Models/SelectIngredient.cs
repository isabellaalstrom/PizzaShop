using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models
{
    public class SelectIngredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public bool IsSelected { get; set; }
    }
}
