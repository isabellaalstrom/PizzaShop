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

        public CartItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CartItem> EditCartItemIngredients(List<Ingredient> checkedIngredients, CartItem cartItem)
        {
            CheckForAddedIngredients(checkedIngredients, cartItem);
            CheckForRemovedIngredients(checkedIngredients, cartItem);
            SetCartItemIsModified(cartItem);
            await Task.CompletedTask;
            return cartItem;
        }

        private void SetCartItemIsModified(CartItem cartItem)
        {
            var dishIngredientsToCompare = cartItem.CartItemIngredients.Select(cartItemIngredient =>
                _context.DishIngredients.First(i => i.Ingredient.IngredientName == cartItemIngredient.IngredientName)).OrderBy(x => x.Ingredient.IngredientName).ToList();
            var dish = _context.Dishes.Include(d => d.DishIngredients).ThenInclude(di => di.Ingredient)
                .FirstOrDefault(p => p.DishId == cartItem.DishId);
            var dishIngredients = dish.DishIngredients.OrderBy(x => x.Ingredient.IngredientName);
            cartItem.IsModified = !dishIngredients.SequenceEqual(dishIngredientsToCompare, new DefaultDishIngredientComparer());
        }

        private void CheckForRemovedIngredients(List<Ingredient> checkedIngredients, CartItem cartItem)
        {
            var toRemove = new List<CartItemIngredient>();
            foreach (var cartItemIngredient in cartItem.CartItemIngredients)
            {
                if (checkedIngredients.All(i => i.IngredientName != cartItemIngredient.IngredientName))
                {
                    toRemove.Add(cartItem.CartItemIngredients.Find(cii => cii.IngredientName == cartItemIngredient.IngredientName));
                }
            }
            foreach (var cartItemIngredient in toRemove)
            {
                cartItem.CartItemIngredients.Remove(cartItemIngredient);
            }
        }

        private void CheckForAddedIngredients(List<Ingredient> checkedIngredients, CartItem cartItem)
        {
            foreach (var checkedIngredient in checkedIngredients)
            {
                if (cartItem.Dish.DishIngredients.All(cii => cii.Ingredient.IngredientName != checkedIngredient.IngredientName)
                    && cartItem.CartItemIngredients.All(cii => cii.IngredientName != checkedIngredient.IngredientName))
                {
                    AddNewCartItemIngredient(cartItem, checkedIngredient, false);
                }
                else if (cartItem.Dish.DishIngredients.Any(cii => cii.Ingredient.IngredientName == checkedIngredient.IngredientName)
                         && cartItem.CartItemIngredients.All(cii => cii.IngredientName != checkedIngredient.IngredientName))
                {
                    AddNewCartItemIngredient(cartItem, checkedIngredient, true);
                }
            }
        }

        private void AddNewCartItemIngredient(CartItem cartItem, Ingredient checkedIngredient, bool isOriginal)
        {
            cartItem.CartItemIngredients.Add(new CartItemIngredient
            {
                CartItemId = cartItem.CartItemId,
                IngredientName = checkedIngredient.IngredientName,
                Price = checkedIngredient.Price,
                IsOriginalIngredient = isOriginal
            });
        }

        public int GetItemPrice(CartItem item)
        {
            var ingredientsSum = item.CartItemIngredients.Sum(x => x.Price);
            return (item.Price + ingredientsSum) * item.Quantity;;
        }
    }
}
