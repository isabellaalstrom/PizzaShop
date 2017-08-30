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
        public virtual void AddItem(CartItem dish)
        {
            //var item = new CartItem
            //{
            //    Dish = dish,
            //    CartItemIngredients = new List<CartItemIngredient>()
            //};
            //foreach (var dishIngredient in dish.DishIngredients)
            //{
            //    var cii = new CartItemIngredient
            //    {
            //        IngredientName = dishIngredient.Ingredient.IngredientName,
            //        Price = dishIngredient.Ingredient.Price,
            //        CartItem = item
            //    };
            //    item.CartItemIngredients.Add(cii);
            //}

            _cartItems.Add(dish);
        }
        public virtual void RemoveItem(Dish dish) => _cartItems.RemoveAll(l => l.Dish.DishId == dish.DishId);

        public virtual decimal ComputeTotalValue() => _cartItems.Sum(e => e.Dish.Price /** e.Quantity*/);

        public virtual void Clear() => _cartItems.Clear();
        public virtual IEnumerable<CartItem> Items => _cartItems;
    }
}
