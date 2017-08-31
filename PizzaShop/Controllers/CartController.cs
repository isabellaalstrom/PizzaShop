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
        private readonly CartItemService _cartItemService;

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
            var cartItemId = 1;
            if (_cart.Items.Any())
            {
                cartItemId = _cart.Items.Count() + 1;
            }
            Dish dish = _context.Dishes
                .FirstOrDefault(p => p.DishId == id);
            var ings = _ingredientService.IngredientByDishId(id);
            dish.DishIngredients = null;
            var item = new CartItem
            {
                CartItemId = cartItemId,
                //Doesn't get a CartItemId - only 0
                //CartId = _cart.CartId,
                Dish = dish,
                CartItemIngredients = new List<CartItemIngredient>()
            };
            foreach (var ingredient in ings)
            {
                item.CartItemIngredients.Add(new CartItemIngredient
                {
                    IngredientName = ingredient.IngredientName,
                    Price = 0,
                    CartItemId = item.CartItemId
                });
            }
            _cart.AddItem(item);

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
        public RedirectToActionResult EditItemIngredients(int id, IFormCollection collection) //id == CartItemId
        {
            //todo ta ut vilken CartItem det gäller
            var checkedIngredientIds = collection.Keys.Where(x => x.StartsWith("ingredient-"));
            var checkedIngredients = new List<Ingredient>();
            foreach (var ingredientId in checkedIngredientIds)
            {
                checkedIngredients.Add(
                    _context.Ingredients.First(x => x.IngredientId == Int32.Parse(ingredientId.Remove(0, 11))));
            }
            foreach (var ingredient in _context.Ingredients)
            {
                var isEnabled = checkedIngredients.Any(cb => cb.IngredientId == ingredient.IngredientId);
                if (isEnabled)
                {
                    //AddCartItemIngredient(cartitemid, ingredient);
                    //
                }
                //kolla mot cartitems riktiga ingredienser om man tagit bort nåt.
                //uppdatera totalpriset i cart?
            }


            //var checkedIngredientIds = collection.Keys.Where(x => x.StartsWith("ingredient-"));
            //var ingredients = new List<Ingredient>();
            //foreach (var ingredientId in checkedIngredientIds)
            //{
            //    ingredients.Add(_context.Ingredients.First(x => x.IngredientId == Int32.Parse(ingredientId.Remove(0, 11))));
            //}
            //var cartItemIngredients = new List<CartItemIngredient>();

            //foreach (var ingredient in ingredients)
            //{
            //    cartItemIngredients.Add(new CartItemIngredient
            //    {
            //        IngredientName = ingredient.IngredientName,
            //        Price = ingredient.Price
            //        //,
            //        //CartItemId = item.CartItemId
            //    });
            //}


            return RedirectToAction("Index");
        }
    }
}