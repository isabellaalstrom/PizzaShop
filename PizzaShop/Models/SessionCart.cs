using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PizzaShop.Entities;
using PizzaShop.Infrastructure;
using PizzaShop.Services;

namespace PizzaShop.Models
{
    public class SessionCart : Cart
    {
        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
            SessionCart cart = session?.GetJson<SessionCart>("Cart")
                               ?? new SessionCart();
            cart.Session = session;
            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }
        public override void AddItem(Dish dish, int quantity)
        {
            base.AddItem(dish, quantity);
            Session.SetJson("Cart", this);
        }
        public override void RemoveItem(CartItem item)
        {
            base.RemoveItem(item);
            Session.SetJson("Cart", this);
        }
        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }

        public override void UpdateItemIngredients(CartItem item)
        {
            base.UpdateItemIngredients(item);
            Session.SetJson("Cart", this);
        }

        public override void UpdateQuantity(CartItem item)
        {
            base.UpdateQuantity(item);
            Session.SetJson("Cart", this);
        }
    }
}
