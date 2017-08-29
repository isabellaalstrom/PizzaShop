using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaShop.Data;
using PizzaShop.Models;

namespace PizzaShop.Services
{
    public class DishTypeService
    {
        private readonly ApplicationDbContext _context;

        public DishTypeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<DishType> All()
        {
            return _context.DishTypes.ToList();
        }
    }
}
