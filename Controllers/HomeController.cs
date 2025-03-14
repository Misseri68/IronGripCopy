using Microsoft.AspNetCore.Mvc;
using IronGrip.Extensions;
using IronGrip.Models;
using Microsoft.Extensions.Caching.Memory;

namespace IronGrip.Controllers
{
    public class HomeController : Controller
    {

        private IMemoryCache memoryCache;
        public HomeController(IMemoryCache memoryCache) {
            this.memoryCache = memoryCache;
        }


        public IActionResult Home()
        {
            Usuario user  = HttpContext.Session.GetObject<Usuario>("USER");
            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }
            if(memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO") != null)
            ViewData["MENSAJE"] = "Todavía tienes un entrenamiento abierto, haz click en 'Nuevo entrenamiento' para seguir con él o cancelarlo";
            return View(user);
        }
    }
}
