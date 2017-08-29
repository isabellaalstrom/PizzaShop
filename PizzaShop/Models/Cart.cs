using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaShop.Data;

namespace PizzaShop.Models
{
    public class Cart
    {
        private List<CartItem> _cartItems = new List<CartItem>();
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
    //private readonly ApplicationDbContext _context;

    //private Cart(ApplicationDbContext context)
    //{
    //    _context = context;
    //}


    //public string CartId { get; set; }
    //public ApplicationUser ApplicationUser { get; set; }
    //public string ApplicationUserId { get; set; }
    //public List<CartItem> CartItems { get; set; }

    //public static Cart GetCart(IServiceProvider services)
    //{
    //    ISession session = services.GetRequiredService<IHttpContextAccessor>()?
    //        .HttpContext.Session;

    //    var context = services.GetService<ApplicationDbContext>();
    //    string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

    //    session.SetString("CartId", cartId);

    //    return new Cart(context) { CartId = cartId };
    //}

    //public void AddToCart(Dish dish, int Quantity)
    //{
    //    var CartItem =
    //        _context.CartItems.SingleOrDefault(
    //            s => s.Dish.DishId == dish.DishId && s.CartId == CartId);

    //    if (CartItem == null)
    //    {
    //        CartItem = new CartItem
    //        {
    //            CartId = CartId,
    //            Dish = dish,
    //            Quantity = 1
    //            //todo add also cartitemingredients
    //        };
    //        _context.CartItems.Add(CartItem);
    //    }
    //    else
    //    {
    //        CartItem.Quantity++;
    //    }
    //    _context.SaveChanges();
    //}

    //public int RemoveFromCart(Dish dish)
    //{
    //    var CartItem =
    //        _context.CartItems.SingleOrDefault(
    //            s => s.Dish.DishId == dish.DishId && s.CartId == CartId);

    //    var localQuantity = 0;

    //    if (CartItem != null)
    //    {
    //        if (CartItem.Quantity > 1)
    //        {
    //            CartItem.Quantity--;
    //            localQuantity = CartItem.Quantity;
    //        }
    //        else
    //        {
    //            _context.CartItems.Remove(CartItem);
    //        }
    //    }

    //    _context.SaveChanges();

    //    return localQuantity;
    //}

    //public List<CartItem> GetCartItems()
    //{
    //    return CartItems ??
    //           (CartItems =
    //               _context.CartItems.Where(c => c.CartId == CartId)
    //                   .Include(s => s.Dish)
    //                   .ToList());
    //}

    //public void ClearCart()
    //{
    //    var cartItems = _context
    //        .CartItems
    //        .Where(cart => cart.CartId == CartId);

    //    _context.CartItems.RemoveRange(cartItems);

    //    _context.SaveChanges();
    //}



    //public int GetCartTotal()
    //{
    //    var total = _context.CartItems.Where(c => c.CartId == CartId)
    //        .Select(c => c.Dish.Price * c.Quantity).Sum();
    //    return total;
    //}
}
