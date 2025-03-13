using IronGrip.Extensions;
using IronGrip.Models;
using IronGrip.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IronGrip.Controllers
{
    public class TagsController : Controller
    {
     
        TagRepository repo;
        public TagsController(TagRepository repo) {
           
            this.repo = repo;
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(string nombre, string color)
        {
            if(nombre == null || color == null)
            {
                ViewData["MENSAJE"] = "Eres retrasado o que?";
                return View();
            }
            Usuario user = HttpContext.Session.GetObject<Usuario>("USER");
            await this.repo.CreateTagAsync(user.Id, nombre, color);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index()
        {
            Usuario user = HttpContext.Session.GetObject<Usuario>("USER");
            List<Tag> tags = await this.repo.GetTagsAsync(user.Id);
            return View(tags);
        }
    }
}
