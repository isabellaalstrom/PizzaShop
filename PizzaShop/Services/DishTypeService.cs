using System.Collections.Generic;
using System.Linq;
using PizzaShop.Data;
using PizzaShop.Entities;

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
