using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Services;

namespace PizzaShop.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        private readonly List<CartItem> _cartItems = new List<CartItem>();
        public virtual void AddItem(Dish dish, int quantity)
        {
            var cartItemId = 1;
            if (_cartItems.Any())
            {
                cartItemId = _cartItems.Count + 1;
            }
            var item = new CartItem
            {
                CartItemId = cartItemId,
                Dish = dish,
                CartItemIngredients = new List<CartItemIngredient>(),
                Price = dish.Price,
                Quantity = quantity
            };
            foreach (var dishIngredient in dish.DishIngredients)
            {
                item.CartItemIngredients.Add(new CartItemIngredient
                {
                    IngredientName = dishIngredient.Ingredient.IngredientName,
                    Price = 0,
                    CartItemId = item.CartItemId
                });
            }
            _cartItems.Add(item);
        }
        public virtual void RemoveItem(CartItem item) => _cartItems.RemoveAll(ci => ci.CartItemId == item.CartItemId);

        public virtual decimal ComputeTotalValue() => _cartItems.Sum(e => e.Price * e.Quantity);

        public virtual void Clear() => _cartItems.Clear();
        public virtual IEnumerable<CartItem> Items => _cartItems;

        public virtual void UpdateItemIngredients(CartItem updatedItem)
        {
            if (_cartItems.Exists(ci => ci.CartItemId == updatedItem.CartItemId))
            {
                var oldItem = _cartItems.First(ci => ci.CartItemId == updatedItem.CartItemId);
                _cartItems.Remove(oldItem);
                _cartItems.Add(updatedItem);
            }
        }

        public virtual void UpdateQuantity(CartItem item)
        {
            var itemToUpdate = _cartItems.FirstOrDefault(ci => ci.CartItemId == item.CartItemId);
            if (itemToUpdate!=null)
            {
                itemToUpdate.Quantity++;
            }
        }
    }
}
