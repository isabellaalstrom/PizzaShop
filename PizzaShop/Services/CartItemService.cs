using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Infrastructure;

namespace PizzaShop.Services
{
    public class CartItemService
    {
        private readonly ApplicationDbContext _context;

        public CartItemService(ApplicationDbContext context, ICartService cartService)
        {
            _context = context;
        }

        public async Task<CartItem> EditCartItemIngredients(IEnumerable<string> checkedIngredientIds, CartItem cartItem)
        {
            var checkedIngredients = checkedIngredientIds.Select(ingredientId =>
                _context.Ingredients.First(x => x.IngredientId == int.Parse(ingredientId.Remove(0, 11)))).ToList();
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
                        IngredientName = checkedIngredient.IngredientName,
                        IsOriginalIngredient = true
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
            await Task.CompletedTask;
            return cartItem;
        }
    }
}
