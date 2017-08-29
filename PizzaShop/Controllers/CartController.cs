using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;
using PizzaShop.Models.CartViewModels;

namespace PizzaShop.Controllers
{
    public class CartController : Controller
    {
        private ApplicationDbContext _context;
        private readonly Cart _cart;
        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int id, string returnUrl)
        {
            Dish dish= _context.Dishes
                .FirstOrDefault(p => p.DishId == id);
            if (dish != null)
            {
                _cart.AddItem(dish, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToActionResult RemoveFromCart(int id,
            string returnUrl)
        {
            Dish dish = _context.Dishes
                .FirstOrDefault(p => p.DishId == id);
            if (dish != null)
            {
                _cart.RemoveItem(dish);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
