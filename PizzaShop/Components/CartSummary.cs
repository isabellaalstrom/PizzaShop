using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;
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
