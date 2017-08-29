using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaShop.Data;
using PizzaShop.Entities;

namespace PizzaShop.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        private readonly List<CartItem> _cartItems = new List<CartItem>();
        public virtual void AddItem(Dish dish, int quantity)
        {
            CartItem item = _cartItems
                .FirstOrDefault(p => p.Dish.DishId == dish.DishId);
            if (item == null)
            {
                _cartItems.Add(new CartItem
                {
                    Dish = dish,
                    Quantity = quantity
                });
            }
            else
            {
                item.Quantity += quantity;
            }
        }
        public virtual void RemoveItem(Dish dish) => _cartItems.RemoveAll(l => l.Dish.DishId == dish.DishId);

        public virtual decimal ComputeTotalValue() => _cartItems.Sum(e => e.Dish.Price * e.Quantity);

        public virtual void Clear() => _cartItems.Clear();
        public virtual IEnumerable<CartItem> Items => _cartItems;
    }
}
