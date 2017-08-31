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
            var cartItemId = 1;
            if (_cart.Items.Any())
            {
                cartItemId = _cart.Items.Count() + 1;
            }
            Dish dish = _context.Dishes/*.Include(x => x.DishIngredients).ThenInclude(x => x.Ingredient)*/
                .FirstOrDefault(p => p.DishId == id);
            var ings = _ingredientService.IngredientByDishId(id);
            //dish.DishIngredients = null;
            var item = new CartItem
            {
                CartItemId = cartItemId,
                Dish = dish,
                CartItemIngredients = new List<CartItemIngredient>(),
                Price = dish.Price
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
            var cartItem = _cart.Items.First(ci => ci.CartItemId == id);
            if (cartItem != null)
            {
                _cart.RemoveItem(cartItem);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public RedirectToActionResult EditItemIngredients(int id, IFormCollection collection)
        {
            var checkedIngredientIds = collection.Keys.Where(x => x.StartsWith("ingredient-"));
            var checkedIngredients = checkedIngredientIds.Select(ingredientId => 
            _context.Ingredients.First(x => x.IngredientId == Int32.Parse(ingredientId.Remove(0, 11)))).ToList();

            var cartItem = _cart.Items.First(ci => ci.CartItemId == id);
            foreach (var ingredient in checkedIngredients)
            {
                if (!cartItem.Dish.DishIngredients.Any(cii => cii.Ingredient.IngredientName == ingredient.IngredientName)
                    && !cartItem.CartItemIngredients.Any(cii => cii.IngredientName == ingredient.IngredientName))
                {
                    //cartItem.CartItemIngredients.Add(new CartItemIngredient
                    //{
                    //    CartItemId = cartItem.CartItemId,
                    //    IngredientName = ingredient.IngredientName,
                    //    Price = 0
                    //});
                //}
                //else
                //{
                    cartItem.CartItemIngredients.Add(new CartItemIngredient
                    {
                        CartItemId = cartItem.CartItemId,
                        IngredientName = ingredient.IngredientName,
                        Price = ingredient.Price
                    });
                    cartItem.Price += ingredient.Price; /*cartItem.CartItemIngredients.Sum(itemIngredient => itemIngredient.Price);*/
                }
            }
            //todo kvar att göra - ta bort ingrediens
            var oldItem = _cart.Items.First(ci => ci.CartItemId == cartItem.CartItemId);

            _cart.RemoveItem(oldItem);
            _cart.AddItem(cartItem);

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