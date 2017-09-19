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
    [Authorize(Roles = "Admin")]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly ICartService _cartService;

        public PaymentsController(ApplicationDbContext context, IEmailSender emailSender, ICartService cartService, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _cartService = cartService;
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
        [AllowAnonymous]
        public IActionResult Create(Order order)
        {
            //var order = _context.Orders.FirstOrDefault(o => o.OrderId == id);
            var model = new CreatePaymentViewModel
            {
                OrderId = order.OrderId,
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
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreatePaymentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = model.Order;
                order.OrderCartItems = _context.CartItems.Where(ci => ci.CartId == _cartService.GetCart().CartId).ToList();
                if (order.UserId != null)
                {
                   order.User = await _userManager.FindByIdAsync(order.UserId); 
                }
                
                var payment = new Payment
                {
                    Order = order,
                    OrderId = model.OrderId,
                    Amount = model.Amount,
                    CardHolder = model.CardHolder,
                    CreditCardNumber = model.CreditCardNumber,
                    Cvv = model.Cvv,
                    ExpireMonth = model.ExpireMonth,
                    ExpireYear = model.ExpireYear
                };
                order.Payment = payment;
                order.IsPayed = true;
                _context.Add(order);
                _context.Add(payment);
                _cartService.ClearCart();
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ThankYou), new { name = payment.Order.Name, amount = payment.Amount, email = payment.Order.Email } );
            }
            return View(model);
        }

        [AllowAnonymous]
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
