using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Infrastructure;
using PizzaShop.Models;

namespace PizzaShop.Services
{
    public class CartService : ICartService
    {
        private readonly ISession _session;

        public ApplicationDbContext _context;

        public CartService(ISession session, ApplicationDbContext context)
        {
            _session = session;
            _context = context;
        }

        public virtual Cart GetCart()
        {
            var cart = _session?.GetJson<Cart>("Cart") ?? new Cart();
            return cart;
        }

        public void AddToCart(Dish dish, int quantity)
        {
            var cart = GetCart();
            var item = new CartItem
            {
                Dish = dish,
                DishId = dish.DishId,
                CartItemIngredients = new List<CartItemIngredient>(),
                Price = dish.Price,
                Quantity = quantity,
                //CartItemId = cart.CartId + dish.DishId + cart.CartItems.Count,
                CartItemName = dish.DishName
            };
            foreach (var dishIngredient in dish.DishIngredients)
            {
                var cartItemIngredient = new CartItemIngredient
                {
                    IngredientName = dishIngredient.Ingredient.IngredientName,
                    Price = 0,
                    CartItem = item,
                    IsOriginalIngredient = true
                };
                item.CartItemIngredients.Add(cartItemIngredient);
                _context.CartItemIngredients.Add(cartItemIngredient);
            }
            _context.CartItems.Add(item);
            _context.SaveChanges();
            cart.CartItems.Add(item);
            SaveCart(cart);
        }

        public void UpdateItemIngredients(CartItem updatedItem)
        {
            var cart = GetCart();
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
            var cart = GetCart();
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
            var cart = GetCart();
            cart.CartItems.RemoveAll(ci => ci.CartItemId == item.CartItemId);
            SaveCart(cart);
        }

        public void ClearCart()
        {
            var cart = GetCart();
            cart.CartItems.Clear();
            _session.Remove("Cart");
        }

        public virtual bool SaveCart(Cart cart)
        {
            _session.SetJson("Cart", cart);
            return true;
        }

        public int ComputeTotalValue()
        {
            int sum = 0;
            foreach (var item in GetCart().CartItems)
            {
                var ingredientsSum = item.CartItemIngredients.Sum(x => x.Price);
                sum += (item.Price + ingredientsSum) * item.Quantity;
            }
        return sum;
        } 
        
    }
}
