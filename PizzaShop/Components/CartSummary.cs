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
        private Cart cart;
        public CartSummary(Cart cartService)
        {
            cart = cartService;
        }
        public IViewComponentResult Invoke()
        {
            return View(cart);
        }
    }
}
