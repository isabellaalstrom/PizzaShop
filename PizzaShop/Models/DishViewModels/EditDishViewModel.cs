using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models.DishViewModels
{
    public class EditDishViewModel
    {
        [Display(Name = "Maträtt-id")]
        public int DishId { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett namn på rätten"), Display(Name = "Namn")]
        public string DishName { get; set; }
        [Required(ErrorMessage = "Du måste fylla i ett pris"), Display(Name = "Pris")]
        public string Price { get; set; }
        [Display(Name = "Ingredienser")]
        public virtual ICollection<DishIngredient> DishIngredients { get; set; }
        [Display(Name = "Ingredienser")]
        public List<SelectIngredient> SelectIngredients { get; set; }
    }
}
