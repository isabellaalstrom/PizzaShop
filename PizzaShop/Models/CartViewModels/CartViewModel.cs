using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaShop.Models.CartViewModels
{
    public class CartViewModel
    {
        public Cart Cart { get; set; }
        public int TotalAmount { get; set; }
        //todo maybe also user info, if logged in?
    }
}
