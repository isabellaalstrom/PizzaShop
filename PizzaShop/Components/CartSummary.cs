using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.Models.CartViewModels;

namespace PizzaShop.Components
{
    public class CartSummary : ViewComponent
    {
        public readonly Cart _cart;

        public CartSummary(Cart cart)
        {
            _cart = cart;
        }

        public IViewComponentResult Invoke()
        {
            //var items = _cart.GetCartItems();
            var items = new List<CartItem>() { new CartItem(), new CartItem() };
            _cart.CartItems = items;
            var cartVM = new CartViewModel
            {
                Cart = _cart,
                TotalAmount = _cart.GetCartTotal()
            };
            return View(cartVM);
        }
    }
}
