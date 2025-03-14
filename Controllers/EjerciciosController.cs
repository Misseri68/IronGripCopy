using IronGrip.Extensions;
using IronGrip.Helpers;
using IronGrip.Models;
using IronGrip.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace IronGrip.Controllers
{
    public class EjerciciosController : Controller
    {

        private HelperPathProvider helperPath;
        EjercicioRepository repo;

        public EjerciciosController(HelperPathProvider helperPath, EjercicioRepository repo)
        {
            this.repo = repo;
            this.helperPath = helperPath;
        }
        public async Task<IActionResult> Index()
        {
            int idUsuario = HttpContext.Session.GetObject<Usuario>("USER").Id;
            List<Ejercicio> ejercicios = await this.repo.GetEjerciciosAsync(idUsuario);
            return View(ejercicios);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Create(string nombre, string descripcion,
            bool esTiempo, IFormFile fichero, List<Tag>? tags) { 
            string fileName = fichero.FileName;
            string path = this.helperPath.MapPath(fileName);
            using (Stream stream = new FileStream(path, FileMode.Create))
            {
                await fichero.CopyToAsync(stream);
            }
            int idUsuario = HttpContext.Session.GetObject<Usuario>("USER").Id;
            await this.repo.CreateEjercicioAsync(nombre, descripcion, fileName, esTiempo, idUsuario );
            return RedirectToAction("Index");
        }
    }
}
