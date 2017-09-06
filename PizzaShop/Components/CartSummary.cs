using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
using PizzaShop.Models.CartViewModels;
using PizzaShop.Services;

namespace PizzaShop.Components
{
    public class CartSummary : ViewComponent
    {
        private readonly ICartService _cartService;
        public CartSummary(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IViewComponentResult Invoke()
        {
            Cart cart = _cartService.GetCart();
            return View(cart);
        }
    }
}
