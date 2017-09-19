using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Models;
using PizzaShop.Models.MenuViewModels;

namespace PizzaShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new MenuViewModel();

            IList<DishModel> pastaDishes = new List<DishModel>();
            IList<DishModel> pizzaDishes = new List<DishModel>();
            IList<DishModel> salladDishes = new List<DishModel>();

            TransformDishToModel(pastaDishes, pizzaDishes, salladDishes);

            model.PizzaDishes = pizzaDishes.OrderBy(x => x.Price).ToList();
            model.PastaDishes = pastaDishes.OrderBy(x => x.Price).ToList();
            model.SalladDishes = salladDishes.OrderBy(x => x.Price).ToList();

            return View(model);
        }

        private void TransformDishToModel(IList<DishModel> pastaDishes, IList<DishModel> pizzaDishes, IList<DishModel> salladDishes)
        {
            var result = _context.Dishes
                .Include(i => i.DishType)
                .Include(i => i.DishIngredients)
                .ThenInclude(i => i.Ingredient);


            foreach (var dish in result)
            {
                if (dish.DishType.DishTypeName == "Pizza")
                {
                    pizzaDishes.Add(new DishModel
                    {
                        DishId = dish.DishId,
                        DishName = dish.DishName,
                        Price = dish.Price,
                        Ingredients = dish.DishIngredients.Where(w => w.Ingredient != null).Select(s => s.Ingredient).ToList()
                    });
                }
                if (dish.DishType.DishTypeName == "Pasta")
                {
                    pastaDishes.Add(new DishModel
                    {
                        DishId = dish.DishId,
                        DishName = dish.DishName,
                        Price = dish.Price,
                        Ingredients = dish.DishIngredients.Select(s => s.Ingredient).ToList()
                    });
                }
                if (dish.DishType.DishTypeName == "Salad")
                {
                    salladDishes.Add(new DishModel
                    {
                        DishId = dish.DishId,
                        DishName = dish.DishName,
                        Price = dish.Price,
                        Ingredients = dish.DishIngredients.Select(s => s.Ingredient).ToList()
                    });
                }
            }
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
