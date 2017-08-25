using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Models;
using PizzaShop.Models.CartViewModels;

namespace PizzaShop.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly Cart _cart;

        public CartController(ApplicationDbContext context, Cart cart)
        {
            _context = context;
            _cart = cart;
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var items = _cart.GetCartItems();
            _cart.CartItems = items;

            var cartVM = new CartViewModel
            {
                Cart = _cart,
                TotalAmount = _cart.GetCartTotal()
            };
            //todo user here?
            return View(cartVM);
        }

        public RedirectToActionResult AddToCart(int dishId)
        {
            var selectedDish = _context.Dishes.FirstOrDefault(p => p.DishId == dishId);

            if (selectedDish != null)
            {
                _cart.AddToCart(selectedDish, 1);
            }
            return RedirectToAction("Index");
        }
        //// GET: Cart/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Cart
        //        .Include(c => c.ApplicationUser)
        //        .SingleOrDefaultAsync(m => m.CartId == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        //// GET: Cart/Create
        //public IActionResult Create()
        //{
        //    ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}

        //// POST: Cart/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CartId,ApplicationUserId")] Cart cart)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(cart);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", cart.ApplicationUserId);
        //    return View(cart);
        //}

        //// GET: Cart/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Cart.SingleOrDefaultAsync(m => m.CartId == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", cart.ApplicationUserId);
        //    return View(cart);
        //}

        //// POST: Cart/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("CartId,ApplicationUserId")] Cart cart)
        //{
        //    if (id != cart.CartId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(cart);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CartExists(cart.CartId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "Id", cart.ApplicationUserId);
        //    return View(cart);
        //}

        //// GET: Cart/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var cart = await _context.Cart
        //        .Include(c => c.ApplicationUser)
        //        .SingleOrDefaultAsync(m => m.CartId == id);
        //    if (cart == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(cart);
        //}

        //// POST: Cart/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var cart = await _context.Cart.SingleOrDefaultAsync(m => m.CartId == id);
        //    _context.Cart.Remove(cart);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CartExists(string id)
        //{
        //    return _context.Cart.Any(e => e.CartId == id);
        //}

    }
}
