using IronGrip.Extensions;
using IronGrip.Models;
using IronGrip.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace IronGrip.Controllers
{
    public class EjerciciosHechosController : Controller
    {
        private IMemoryCache memoryCache;
        private EjercicioRepository repoEjercicio;
        EjercicioHechoRepository repo;

        public EjerciciosHechosController(IMemoryCache memoryCache, EjercicioRepository repoEjercicio
            , EjercicioHechoRepository repo)
        {
            this.memoryCache = memoryCache;
            this.repo = repo;
            this.repoEjercicio = repoEjercicio;
        }


        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            int idUsuario = HttpContext.Session.GetObject<Usuario>("USER").Id;

            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            if(vista.EjerciciosHechosModel == null)
            {
                vista.EjerciciosHechosModel = new List<PartialEjercicioModel>();
                this.memoryCache.Set("NEWENTRENAMIENTO", vista);
            }
            List<Ejercicio> ejercicios = await this.repoEjercicio.GetEjerciciosAsync(idUsuario);
            return View(ejercicios);
        }


        [HttpPost]
        public async Task<IActionResult> Create(int ejercicio)
        {
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            PartialEjercicioModel ejModel = new PartialEjercicioModel();
            ejModel.EjercicioHecho = new EjercicioHecho();
            ejModel.EjercicioHecho.IdEjercicio = ejercicio;
            ejModel.EjercicioHecho.IdEntrenamiento = vista.Entrenamiento.Id;
            ejModel.EjercicioHecho.Id = await GetMaxIdCache();
            ejModel.Series = new List<Serie>();
            Ejercicio ej = await this.repoEjercicio.FindEjercicioAsync(ejercicio);
            ejModel.Ejercicio = ej;
            vista.EjerciciosHechosModel.Add(ejModel);
            this.memoryCache.Set("NEWENTRENAMIENTO", vista);
            return RedirectToAction("CreateSeries", new {idModel = ejModel.EjercicioHecho.Id});
        }

        public async Task<IActionResult> CreateSeries(int idModel)
        {
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            PartialEjercicioModel ejModel = vista.EjerciciosHechosModel.Find(x => x.EjercicioHecho.Id == idModel);
            return View(ejModel);

        }


        [HttpPost]
        public async Task<IActionResult> CreateSeries(Serie serie, int idModel)
        {
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            PartialEjercicioModel ejModel = vista.EjerciciosHechosModel.Find(x => x.EjercicioHecho.Id == idModel);
                serie.Id = await GetMaxIdCacheSeries();
                serie.IdEjercicioHecho = ejModel.EjercicioHecho.Id;
                ejModel.Series.Add(serie);
            this.memoryCache.Set("NEWENTRENAMIENTO", vista);
            return View(ejModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateSeries(Serie serie, int idModel)
        {
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            PartialEjercicioModel ejModel = vista.EjerciciosHechosModel.Find(x => x.EjercicioHecho.Id == idModel);

            int index  =  ejModel.Series.FindIndex(x => x.Id == serie.Id);
            ejModel.Series[index] = serie;
            this.memoryCache.Set("NEWENTRENAMIENTO", vista);
            return RedirectToAction("CreateSeries", new {idModel = idModel});
        }

        public IActionResult DeleteEjercicio(int id)
        {
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            PartialEjercicioModel ejModel = vista.EjerciciosHechosModel.Find(x => x.EjercicioHecho.Id == id);
            vista.EjerciciosHechosModel.Remove(ejModel);
            this.memoryCache.Set("NEWENTRENAMIENTO", vista);
            return RedirectToAction("Create", "Entrenamientos");
        }

        public async Task<int> GetMaxIdCache() {
            int maxNum = await this.repo.GetMaxIdAsync();
            //Si ya hay ejercicios en el cache pero no en la base de datos, dará un error por el Id, 
            //Se podría realizar un conteo de los ejercicios que hay, y en base al count, sumar ese numero
            //por el max Id que haya en la base de datos.
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            int conteo = vista.EjerciciosHechosModel.Count();
            return maxNum + conteo;
        }

        //!!TODO ver si funciona
        public async Task<int> GetMaxIdCacheSeries()
        {
            int maxNum = await this.repo.GetMaxIdSerieAsync();
            int conteo = 0;
            VistaEntrenamiento vista = this.memoryCache.Get<VistaEntrenamiento>("NEWENTRENAMIENTO");
            foreach (PartialEjercicioModel model in vista.EjerciciosHechosModel) 
            {
                conteo += model.Series.Count;
            }

            return maxNum + conteo;
        }
    }
}
