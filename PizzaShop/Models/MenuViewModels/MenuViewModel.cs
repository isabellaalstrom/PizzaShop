using System.Collections.Generic;
using PizzaShop.Entities;

namespace PizzaShop.Models.MenuViewModels
{
    public class MenuViewModel
    {
        //public Cart Cart { get; set; }
        public IList<DishModel> PastaDishes { get; set; }
        public IList<DishModel> PizzaDishes { get; set; }
        public IList<DishModel> SalladDishes { get; set; }
    }

    public class DishModel
    {
        public int DishId { get; set; }

        public string DishName { get; set; }

        public decimal Price { get; set; }

        public IList<Ingredient> Ingredients { get; set; }
    }
}
