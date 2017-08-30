using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;

namespace PizzaShop.Services
{
    public class IngredientService
    {
        private readonly ApplicationDbContext _context;

        public IngredientService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Ingredient> All()
        {
            return _context.Ingredients.OrderBy(i => i.IngredientName).ToList();
        }

        public List<Ingredient> IngredientByDishId(int id)
        {
            var dishIngredients = _context.DishIngredients
                                            .Include(i => i.Ingredient)
                                            .Where(di => di.DishId == id).ToList();
            var ingredients = new List<Ingredient>();
            foreach (var dishIngredient in dishIngredients)
            {
                ingredients.Add(dishIngredient.Ingredient);
            }
            return ingredients.OrderBy(i => i.IngredientName).ToList();
        }
    }
}
