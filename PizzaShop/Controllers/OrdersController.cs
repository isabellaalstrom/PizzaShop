﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;
using PizzaShop.Services;

namespace PizzaShop.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [AllowAnonymous]
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
                    model.Email = user.Email;
                }
            }
            return View(model);
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Checkout([Bind("OrderDateTime,TotalAmount,UserId,Name,Address,Zipcode,City,Phonenumber,Email,OrderCartItems")] CheckoutViewModel model)
        {
            var cart = _cartService.GetCart();
            if (!ModelState.IsValid) return View(model);
            if (!cart.CartItems.Any())
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                return View(model);
            }
            var order = new Order
            {
                Name = model.Name,
                City = model.City,
                Address = model.Address,
                OrderDateTime = DateTime.Now,
                Phonenumber = model.Phonenumber,
                Zipcode = model.Zipcode,
                TotalAmount = model.TotalAmount,
                OrderCartItems = _context.CartItems.Where(ci => ci.CartId == cart.CartId).ToList(),
                Email = model.Email
            };
            if (model.UserId != null)
            {
                order.UserId = model.UserId;
                order.User = await _userManager.FindByIdAsync(model.UserId);
            }
            return RedirectToAction("Create", "Payments", order );
        }
        
        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orders.Include(o => o.User).Include(c => c.OrderCartItems).ThenInclude(ci => ci.Dish);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
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

        public async Task<IActionResult> SetDelivered(int? id)
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
            
            if (!order.IsDelivered)
            {
                order.IsDelivered = true;
            }
            else
            {
                order.IsDelivered = false;
            }
            
            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        // GET: Orders/Delete/5
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
