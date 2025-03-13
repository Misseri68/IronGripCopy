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
            return View();
        }
    }
}
