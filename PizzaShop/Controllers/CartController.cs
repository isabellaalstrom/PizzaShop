using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Infrastructure;
using PizzaShop.Models;
using PizzaShop.Models.CartViewModels;
using PizzaShop.Services;

namespace PizzaShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICartService _cartService;

        private readonly CartItemService _cartItemService;

        public CartController(ApplicationDbContext context, ICartService cartService, CartItemService cartItemService)
        {
            _context = context;
            _cartService = cartService;
            _cartItemService = cartItemService;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = _cartService.GetCart(),
                ReturnUrl = returnUrl
            });
        }

        public RedirectResult AddToCart(int id, string returnUrl)
        {
            Cart cart = _cartService.GetCart();
            var dish = _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .FirstOrDefault(p => p.DishId == id);

            if (cart.CartItems.Any(ci => ci.Dish.DishId == dish.DishId))
            {
                var cartItems = cart.CartItems.Where(ci => ci.Dish.DishId == dish.DishId);
                foreach (var cartItem in cartItems)
                {
                    if (!cartItem.IsModified)
                    {
                        _cartService.UpdateQuantity(cartItem, 1);
                        return Redirect(returnUrl);
                    }
                }
            }
            _cartService.AddToCart(dish, 1);
            return Redirect(returnUrl);
        }

        public RedirectToActionResult RemoveFromCart(int id,
            string returnUrl)
        {
            var cartItem = _cartService.GetCart().CartItems.First(ci => ci.CartItemId == id);
            if (cartItem != null && cartItem.Quantity == 1)
            {
                _cartService.RemoveFromCart(cartItem);
            }
            else if (cartItem != null && cartItem.Quantity > 1)
            {
                _cartService.UpdateQuantity(cartItem, 0);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public async Task<RedirectToActionResult> EditItemIngredients(int id, IFormCollection collection)
        {
            var checkedIngredientIds = collection.Keys.Where(x => x.StartsWith("ingredient-"));
            var checkedIngredients = checkedIngredientIds.Select(ingredientId =>
                _context.Ingredients.First(x => x.IngredientId == int.Parse(ingredientId.Remove(0, 11)))).ToList();

            var updatedCartItem = await _cartItemService.EditCartItemIngredients(checkedIngredients,
                _cartService.GetCart().CartItems.First(ci => ci.CartItemId == id));

            _cartService.UpdateItemIngredients(updatedCartItem);

            return RedirectToAction("Index");
        }
    }
}