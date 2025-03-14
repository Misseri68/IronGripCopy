using IronGrip.Extensions;
using IronGrip.Models;
using Microsoft.AspNetCore.Mvc;

namespace IronGrip.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Perfil()
        {
            Usuario usuario = HttpContext.Session.GetObject<Usuario>("USER");
            if(usuario != null)
            {
                return View(usuario);

            }
            else
            {
                return RedirectToAction("Login", "Auth");
            }
        }

        public IActionResult Salir()
        {
            HttpContext.Session.Remove("USER");
            return RedirectToAction("Login", "Auth");
        }
    }
}
