using Microsoft.AspNetCore.Mvc;

namespace PizzaShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}