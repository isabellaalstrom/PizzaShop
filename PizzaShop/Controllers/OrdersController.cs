using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;
using PizzaShop.Services;

namespace PizzaShop.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ICartService _cartService;

        public OrdersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICartService cartService)
        {
            _context = context;
            _userManager = userManager;
            _cartService = cartService;

        }
        public async Task<IActionResult> Checkout()
        {
            var cart = _cartService.GetCart();
            if (!cart.CartItems.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                return RedirectToAction("Index", "Cart");
            }
            var currentUsername = HttpContext.User.Identity.Name;

            var model = new CheckoutViewModel
            {
                TotalAmount = _cartService.ComputeTotalValue(),
                OrderCartItems = cart.CartItems
            };

            if (currentUsername != null)
            {
                var user = await _userManager.FindByNameAsync(currentUsername);
                if (user != null)
                {

                    model.User = user;
                    model.Name = user.Name;
                    model.Address = user.Address;
                    model.Zipcode = user.Zipcode;
                    model.City = user.City;
                    model.Phonenumber = user.PhoneNumber;
                    model.UserId = user.Id;
                }
            }
            return View(model);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout([Bind("OrderDateTime,TotalAmount,UserId,Name,Address,Zipcode,City,Phonenumber,OrderCartItems")] CheckoutViewModel model)
        {
            var cart = _cartService.GetCart();
            if (ModelState.IsValid)
            {
                if (!cart.CartItems.Any())
                {
                    ModelState.AddModelError("", "Sorry, your cart is empty!");
                    return View(model);
                }
                var order = new Order
                {
                    Name = model.Name,
                    City = model.City,
                    UserId = model.UserId,
                    Address = model.Address,
                    OrderDateTime = DateTime.Now,
                    Phonenumber = model.Phonenumber,
                    Zipcode = model.Zipcode,
                    TotalAmount = model.TotalAmount,
                    OrderCartItems = cart.CartItems,
                    OrderId = _context.Orders.Count() + 1
                };
                foreach (var cartItem in order.OrderCartItems)
                {
                    cartItem.DishId = null;
                    cartItem.Dish = null;
                }
                _cartService.ClearCart();
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "Payments");
            }
            return View(model);
        }




        // GET: Orders
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.User).Include(c => c.OrderCartItems).ThenInclude(ci => ci.Dish);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderCartItems)
                .ThenInclude(ci => ci.CartItemIngredients)
                .Include(o => o.OrderCartItems)
                .ThenInclude(ci => ci.Dish)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDateTime,TotalAmount,UserId,Name,Address,Zipcode,City,Phonenumber,Delivered")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.User)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
