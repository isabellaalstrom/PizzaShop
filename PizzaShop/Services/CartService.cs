using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PizzaShop.Entities;
using PizzaShop.Infrastructure;
using PizzaShop.Models;

namespace PizzaShop.Services
{
    public class CartService : ICartService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CartService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public virtual Cart GetCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session?.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public void AddToCart(Dish dish, int quantity)
        {
            Cart cart = GetCart();
            var item = new CartItem
            {
                Dish = dish,
                DishId = dish.DishId,
                CartItemIngredients = new List<CartItemIngredient>(),
                Price = dish.Price,
                Quantity = quantity,
                CartItemId = cart.CartId + dish.DishId + cart.CartItems.Count
            };
            foreach (var dishIngredient in dish.DishIngredients)
            {
                item.CartItemIngredients.Add(new CartItemIngredient
                {
                    IngredientName = dishIngredient.Ingredient.IngredientName,
                    Price = 0,
                    CartItem = item,
                    IsOriginalIngredient = true
                });
            }
            cart.CartItems.Add(item);
            SaveCart(cart);
        }

        public void UpdateItemIngredients(CartItem updatedItem)
        {
            Cart cart = GetCart();
            if (cart.CartItems.Exists(ci => ci.CartItemId == updatedItem.CartItemId))
            {
                var oldItem = cart.CartItems.First(ci => ci.CartItemId == updatedItem.CartItemId);
                cart.CartItems.Remove(oldItem);
                cart.CartItems.Add(updatedItem);
            }
            SaveCart(cart);
        }

        public void UpdateQuantity(CartItem item, int quantity)
        {
            Cart cart = GetCart();
            var itemToUpdate = cart.CartItems.FirstOrDefault(ci => ci.CartItemId == item.CartItemId);
            if (itemToUpdate != null && quantity == 1)
            {
                itemToUpdate.Quantity++;
            }
            else if (itemToUpdate != null && quantity == 0)
            {
                itemToUpdate.Quantity = itemToUpdate.Quantity - 1;
            }
            SaveCart(cart);
        }

        public void RemoveFromCart(CartItem item)
        {
            Cart cart = GetCart();
            cart.CartItems.RemoveAll(ci => ci.CartItemId == item.CartItemId);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            Cart cart = GetCart();
            cart.CartItems.Clear();
            _httpContextAccessor.HttpContext.Session.Remove("Cart");
        }

        public virtual bool SaveCart(Cart cart)
        {
            _httpContextAccessor.HttpContext.Session.SetJson("Cart", cart);
            return true;
        }

        public int ComputeTotalValue() => GetCart().CartItems.Sum(e => e.Price * e.Quantity);
        
    }
}
