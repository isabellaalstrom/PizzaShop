using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Data;
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
            return _context.Ingredients.ToList();
        }
    }
}
