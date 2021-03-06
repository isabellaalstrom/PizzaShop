﻿using PizzaShop.Entities;
using PizzaShop.Models;

namespace PizzaShop.Services
{
    public interface ICartService
    {
        Cart GetCart();
        void AddToCart(Dish dish, int quantity);
        void UpdateItemIngredients(CartItem item);
        void UpdateQuantity(CartItem item, int quantity);
        void RemoveFromCart(CartItem item);
        void ClearCart();
        bool SaveCart(Cart cart);
        int ComputeTotalValue();
    }
}
