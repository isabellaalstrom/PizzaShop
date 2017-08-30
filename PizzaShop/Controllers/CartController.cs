using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;
using PizzaShop.Models.CartViewModels;
using PizzaShop.Services;

namespace PizzaShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;
        private readonly IngredientService _ingredientService;
        public CartController(ApplicationDbContext context, Cart cart, IngredientService ingredientService)
        {
            _context = context;
            _cart = cart;
            _ingredientService = ingredientService;
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
            Dish dish = _context.Dishes
                .FirstOrDefault(p => p.DishId == id);
            var ings = _ingredientService.IngredientByDishId(id);
            dish.DishIngredients = null;
            var item = new CartItem
            {
                Dish = dish,
                CartItemIngredients = new List<CartItemIngredient>()
            };
            foreach (var ingredient in ings)
            {
                item.CartItemIngredients.Add(new CartItemIngredient
                {
                    IngredientName = ingredient.IngredientName,
                    Price = ingredient.Price,
                    CartItemId = item.CartItemId
                });
            }
            if (dish != null)
            {
                _cart.AddItem(item);
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public RedirectToActionResult EditItemIngredients(IFormCollection collection)
        {
            var checkedIngredientIds = collection.Keys.Where(x => x.StartsWith("ingredient-"));
            var ingredients = new List<Ingredient>();
            foreach (var ingredientId in checkedIngredientIds)
            {
                ingredients.Add(_context.Ingredients.First(x => x.IngredientId == Int32.Parse(ingredientId.Remove(0, 11))));
            }
            var cartItemIngredients = new List<CartItemIngredient>();

            foreach (var ingredient in ingredients)
            {
                cartItemIngredients.Add(new CartItemIngredient
                {
                    IngredientName = ingredient.IngredientName,
                    Price = ingredient.Price
                    //,
                    //CartItemId = item.CartItemId
                });
            }
            return RedirectToAction("Index");
        }
    }
}
