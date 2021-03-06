﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;

namespace PizzaShop.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DishesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Dishes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes
                                .Include(dt => dt.DishType)
                                .Include(di => di.DishIngredients)
                                .ThenInclude(i => i.Ingredient)
                                .ToListAsync());
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(dt => dt.DishType)
                .Include(di => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DishId,DishName,Price")] Dish dish, IFormCollection collection)
        {
            if (ModelState.IsValid)
            {
                var dishTypeId = Int32.Parse(collection["dishType"]);
                var dishType = _context.DishTypes.FirstOrDefault(dt => dt.DishTypeId == dishTypeId);
                dish.DishType = dishType;
                _context.Add(dish);

                CreateDishIngredientsListAsync(dish, collection.Keys.Where(x => x.StartsWith("ingredient-")));

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dish);
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.Include(dt => dt.DishType).Include(di => di.DishIngredients).SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DishId,DishName,Price")] Dish dish, IFormCollection collection)
        {

            if (id != dish.DishId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var ingredients = new List<Ingredient>();
                    foreach (var key in collection.Keys.Where(x => x.StartsWith("ingredient-")))
                    {
                        ingredients.Add(_context.Ingredients.First(x => x.IngredientId == Int32.Parse(key.Remove(0, 11))));
                    }
                    foreach (var ingredient in ingredients)
                    {
                        if (!DishIngredientExists(dish.DishId, ingredient.IngredientId))
                        {
                            _context.DishIngredients.Add(new DishIngredient
                            {
                                Dish = dish,
                                Ingredient = ingredient
                            });
                        }
                        else
                        {
                            var allIngredientsForThisDish = _context.DishIngredients.Where(di => di.DishId == dish.DishId);

                            foreach (var dishIngredient in allIngredientsForThisDish)
                            {
                                if (!(ingredient == dishIngredient.Ingredient && dish.DishId == dishIngredient.DishId))
                                {
                                    _context.Remove(dishIngredient);
                                    _context.SaveChanges();
                                }
                            }
                        }
                    }
                    var dishTypeId = Int32.Parse(collection["dishType"]);
                    var dishType = _context.DishTypes.FirstOrDefault(dt => dt.DishTypeId == dishTypeId);
                    dish.DishType = dishType;
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.DishId))
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
            return View(dish);
        }


        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .SingleOrDefaultAsync(m => m.DishId == id);
            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.DishId == id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private void CreateDishIngredientsListAsync(Dish dish, IEnumerable<string> ingredientCollection)
        {
            var ingredients = new List<Ingredient>();
            foreach (var key in ingredientCollection)
            {
                ingredients.Add(_context.Ingredients.First(x => x.IngredientId == Int32.Parse(key.Remove(0, 11))));
            }
            foreach (var ingredient in ingredients)
            {
                _context.DishIngredients.Add(new DishIngredient
                {
                    Dish = dish,
                    Ingredient = ingredient
                });
            }
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.DishId == id);
        }

        private bool DishIngredientExists(int dishId, int ingredientId)
        {
            var existingDishIngredients = _context.DishIngredients.Where(d => d.DishId == dishId).Where(i => i.IngredientId == ingredientId);
            return existingDishIngredients.Any();
        }
    }
}
