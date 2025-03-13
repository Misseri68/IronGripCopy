using Microsoft.AspNetCore.Mvc;
using IronGrip.Extensions;
using IronGrip.Models;

namespace IronGrip.Controllers
{
    public class HomeController : Controller
    {
    

        public HomeController() {
        }


        public IActionResult Home()
        {
            Usuario user  = HttpContext.Session.GetObject<Usuario>("USER");
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            return View(user);
        }
    }
}
