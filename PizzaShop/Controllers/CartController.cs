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

        public CartController(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
            _cartService = cartService;
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
                    var dishIngredientsToCompare = cartItem.CartItemIngredients.Select(cartItemIngredient =>
                    _context.DishIngredients.First(i => i.Ingredient.IngredientName == cartItemIngredient.IngredientName)).ToList();

                    if (dish.DishIngredients.SequenceEqual(dishIngredientsToCompare, new DefaultDishIngredientComparer()))//if true finns en likadan ci redan, lägg på en på quantity istället
                    {
                        _cartService.UpdateQuantity(cartItem);
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
            //todo ta bort en quantity
            var cartItem = _cartService.GetCart().CartItems.First(ci => ci.CartItemId == id);
            if (cartItem != null)
            {
                _cartService.RemoveFromCart(cartItem);

            }
            return RedirectToAction("Index", new { returnUrl });
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public RedirectToActionResult EditItemIngredients(int id, IFormCollection collection)
        {
            //todo refactor
            //todo if två likadana cartitems, ändra bara i ena?
            var checkedIngredientIds = collection.Keys.Where(x => x.StartsWith("ingredient-"));
            var checkedIngredients = checkedIngredientIds.Select(ingredientId =>
            _context.Ingredients.First(x => x.IngredientId == int.Parse(ingredientId.Remove(0, 11)))).ToList();

            var cartItem = _cartService.GetCart().CartItems.First(ci => ci.CartItemId == id);

            //LÄGGA TILL
            //gå igenom alla valda ing
            foreach (var checkedIngredient in checkedIngredients)
            {
                //om ing inte redan var vald
                //och ing inte finns i originalingredienserna
                //lägg till den och plussa priset
                if (!cartItem.Dish.DishIngredients.Any(cii => cii.Ingredient.IngredientName == checkedIngredient.IngredientName)
                    && !cartItem.CartItemIngredients.Any(cii => cii.IngredientName == checkedIngredient.IngredientName))
                {
                    cartItem.CartItemIngredients.Add(new CartItemIngredient
                    {
                        CartItemId = cartItem.CartItemId,
                        IngredientName = checkedIngredient.IngredientName,
                        Price = checkedIngredient.Price
                    });
                    cartItem.Price += checkedIngredient.Price;
                }
                //ing är originalingrediens
                //men finns inte i cii-lista (dvs bortvald förut men vill läggas till igen)
                //addera inte på priset
                else if (cartItem.Dish.DishIngredients.Any(cii => cii.Ingredient.IngredientName == checkedIngredient.IngredientName)
                         && !cartItem.CartItemIngredients.Any(cii => cii.IngredientName == checkedIngredient.IngredientName))
                {
                    cartItem.CartItemIngredients.Add(new CartItemIngredient
                    {
                        CartItemId = cartItem.CartItemId,
                        IngredientName = checkedIngredient.IngredientName
                    });
                }
            }

            //TA BORT
            //ingrediens finns i originaldish
            //men inte i checked
            //ta bort men dra inte ner pris
            foreach (var originalIngredient in cartItem.Dish.DishIngredients)
            {
                if (!checkedIngredients.Any(i => i.IngredientName == originalIngredient.Ingredient.IngredientName))
                {
                    cartItem.CartItemIngredients.Remove(cartItem.CartItemIngredients.Find(
                        cii => cii.IngredientName == originalIngredient.Ingredient.IngredientName));
                }
            }

            //finns i listan över cii
            //men inte checked
            //ta bort och dra ner pris
            var toRemove = new List<CartItemIngredient>();
            foreach (var cartItemIngredient in cartItem.CartItemIngredients)
            {
                if (!checkedIngredients.Any(i => i.IngredientName == cartItemIngredient.IngredientName))
                {
                    cartItem.Price = cartItem.Price - cartItemIngredient.Price;
                    //cartItem.CartItemIngredients.Remove(cartItem.CartItemIngredients.Find(
                    //    cii => cii.IngredientName == cartItemIngredient.IngredientName));
                    toRemove.Add(cartItem.CartItemIngredients.Find(cii => cii.IngredientName == cartItemIngredient.IngredientName));
                }
            }
            foreach (var cartItemIngredient in toRemove)
            {
                cartItem.CartItemIngredients.Remove(cartItemIngredient);
            }


            var dishIngredientsToCompare = cartItem.CartItemIngredients.Select(cartItemIngredient =>
                _context.DishIngredients.First(i => i.Ingredient.IngredientName == cartItemIngredient.IngredientName)).ToList();
            var dish = _context.Dishes
                .Include(d => d.DishIngredients)
                .ThenInclude(di => di.Ingredient)
                .FirstOrDefault(p => p.DishId == cartItem.DishId);
            cartItem.IsModified = !dish.DishIngredients.SequenceEqual(dishIngredientsToCompare, new DefaultDishIngredientComparer());

            _cartService.UpdateItemIngredients(cartItem);

            return RedirectToAction("Index");
        }
    }
}