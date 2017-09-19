using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishTypesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishTypesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DishTypes
        public async Task<IActionResult> Index()
        {
            ViewBag.Error = TempData["Error"];
            return View(await _context.DishTypes.ToListAsync());
        }

        // GET: DishTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishType = await _context.DishTypes
                .SingleOrDefaultAsync(m => m.DishTypeId == id);
            if (dishType == null)
            {
                return NotFound();
            }

            return View(dishType);
        }

        // GET: DishTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DishTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishTypeId,DishTypeName")] DishType dishType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dishType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dishType);
        }

        // GET: DishTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishType = await _context.DishTypes.SingleOrDefaultAsync(m => m.DishTypeId == id);
            if (dishType == null)
            {
                return NotFound();
            }
            return View(dishType);
        }

        // POST: DishTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishTypeId,DishTypeName")] DishType dishType)
        {
            if (id != dishType.DishTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dishType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishTypeExists(dishType.DishTypeId))
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
            return View(dishType);
        }

        // GET: DishTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dishType = await _context.DishTypes.Include(x => x.Dishes)
                .SingleOrDefaultAsync(m => m.DishTypeId == id);
            if (dishType == null)
            {
                return NotFound();
            }
            if (dishType.Dishes.Any())
            {
               TempData["Error"] = "Some dishes have this dish type, therefore it cannot be removed.";
               return RedirectToAction("Index");
            }
            
            return View(dishType);
        }

        // POST: DishTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dishType = await _context.DishTypes.SingleOrDefaultAsync(m => m.DishTypeId == id);
            _context.DishTypes.Remove(dishType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DishTypeExists(int id)
        {
            return _context.DishTypes.Any(e => e.DishTypeId == id);
        }
    }
}
