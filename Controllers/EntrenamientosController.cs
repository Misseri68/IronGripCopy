using IronGrip.Extensions;
using IronGrip.Models;
using IronGrip.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;

namespace IronGrip.Controllers
{
    public class EntrenamientosController : Controller
    {
        private IMemoryCache memoryCache;
        private EntrenamientoRepository repo;
        private TagRepository tagRepo;

        public EntrenamientosController(IMemoryCache memoryCache, EntrenamientoRepository repo, TagRepository tagRepo) 
        {
            this.repo = repo;
            this.tagRepo = tagRepo; ;
            this.memoryCache = memoryCache;
        }

        public async Task<IActionResult> Index()
        {
            int idUsuario = HttpContext.Session.GetObject<Usuario>("USER").Id;
            List<Entrenamiento> entrenamientos = await this.repo.GetEntrenamientosAsync(idUsuario);
            return View(entrenamientos);
        }

        public async Task<IActionResult> Create()
        {
            int idUsuario = HttpContext.Session.GetObject<Usuario>("USER").Id;
            VistaEntrenamiento model;
            if(memoryCache.Get("NEWENTRENAMIENTO") == null)
            {
                model = new VistaEntrenamiento();
                model.Id = await this.repo.GetMaxIdAsync();
                model.Entrenamiento.IdUsuario = idUsuario;
                memoryCache.Set("NEWENTRENAMIENTO", model);
            }
            else
            {
                model = memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            }

            model.TagsDisponibles = await this.tagRepo.GetTagsAsync(idUsuario);

            return View(model);
        }

      
        public async Task<IActionResult> GuardarEntrenamiento() 
        {
            VistaEntrenamiento model;
            if (memoryCache.Get("NEWENTRENAMIENTO") == null)
            {
                return RedirectToAction("Create");
            }else
            {
                model = memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
                await this.repo.InsertFullEntrenamientoAsync(model);
                return RedirectToAction("Index");
            }
        }

        public IActionResult Cancelar()
        {
            this.memoryCache.Remove("NEWENTRENAMIENTO");
            return RedirectToAction("Home", "Home");
        }

        [HttpPost]
        public IActionResult UpdateTags(string tagsSeleccionadosIds)
        {
            var vista = memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            if (vista != null)
            {
                if (vista.SelectedTags != null)
                {
                    vista.SelectedTags = tagsSeleccionadosIds?
                   .Split(',', StringSplitOptions.RemoveEmptyEntries)
                   .Select(int.Parse).ToList();

                    memoryCache.Set("NEWENTRENAMIENTO", vista);
                }
                else
                {
                    vista.SelectedTags = new List<int>();
                    memoryCache.Set("NEWENTRENAMIENTO", vista);
                }
            }
              
            return RedirectToAction("Create");
        }


    }
}
