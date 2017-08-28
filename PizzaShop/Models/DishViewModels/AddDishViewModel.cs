using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models.DishViewModels
{
    public class AddDishViewModel
    {
        [Required(ErrorMessage = "Fyll i ett namn på rätten"), Display(Name = "Namn")]
        public string DishName { get; set; }
        [Required(ErrorMessage = "Fyll i ett pris"), Display(Name = "Pris")]
        public string Price { get; set; }
        [Display(Name = "Ingredienser")]
        public virtual ICollection<DishIngredient> DishIngredients { get; set; }
    }
}
