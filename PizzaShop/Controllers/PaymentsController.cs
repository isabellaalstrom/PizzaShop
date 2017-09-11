﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Data;
using PizzaShop.Entities;
using PizzaShop.Models;
using PizzaShop.Services;

namespace PizzaShop.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public PaymentsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Payments1
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Payment.Include(p => p.Order);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Order)
                .SingleOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments1/Create
        public IActionResult Create(int id)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            var model = new CreatePaymentViewModel
            {
                OrderId = id,
                Order = order,
                CardHolder = order.Name,
                Amount = order.TotalAmount
            };
            return View(model);
        }

        // POST: Payments1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentId,CardHolder,CreditCardNumber,ExpireMonth,ExpireYear,Cvv,OrderId,Amount")] CreatePaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var payment = new Payment
                {
                    Order = _context.Orders.Include(o => o.User).FirstOrDefault(o => o.OrderId == model.OrderId),
                    OrderId = model.OrderId,
                    Amount = model.Amount,
                    CardHolder = model.CardHolder,
                    CreditCardNumber = model.CreditCardNumber,
                    Cvv = model.Cvv,
                    ExpireMonth = model.ExpireMonth,
                    ExpireYear = model.ExpireYear
                };
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYou), new { name = payment.Order.Name, amount = payment.Amount, email = payment.Order.User.Email } );
            }
            return View(model);
        }

        public async Task<ViewResult> ThankYou(string name, int amount, string email)
        {
            var model = new ThankYouViewModel
            {
                Name = name,
                Amount = amount,
                Email = email
            };
            await _emailSender.SendEmailAsync(email, "Thank you for shopping at PizzaShop!",
                "Your dish will be ready for delivery shortly.");

            return View(model);
        }



        // GET: Payments1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment.SingleOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Address", payment.OrderId);
            return View(payment);
        }

        // POST: Payments1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentId,CardHolder,CreditCardNumber,ExpireMonth,ExpireYear,Cvv,OrderId")] Payment payment)
        {
            if (id != payment.PaymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.PaymentId))
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
            ViewData["OrderId"] = new SelectList(_context.Orders, "OrderId", "Address", payment.OrderId);
            return View(payment);
        }

        // GET: Payments1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payment
                .Include(p => p.Order)
                .SingleOrDefaultAsync(m => m.PaymentId == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payment.SingleOrDefaultAsync(m => m.PaymentId == id);
            _context.Payment.Remove(payment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.PaymentId == id);
        }
    }
}
