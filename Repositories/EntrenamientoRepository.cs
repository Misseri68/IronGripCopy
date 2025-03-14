using IronGrip.Data;
using IronGrip.Models;
using Microsoft.EntityFrameworkCore;

namespace IronGrip.Repositories
{
    public class EntrenamientoRepository
    {

        private IronGripContext context;

        public EntrenamientoRepository(IronGripContext context)
        {
            this.context = context;
        }
        
  
        public async Task<List<Entrenamiento>> GetEntrenamientosAsync(int idUsuario)
        {
            return await this.context.Entrenamientos
                .Where(x => x.IdUsuario == idUsuario).ToListAsync();
        }

        public async Task<PartialEjercicioModel> GetEjercicioEntrenamientoAsync(int idEjerHecho)
        {
            Ejercicio ejer = await this.context.Ejercicios
                .Where(x => x.Id == idEjerHecho).FirstOrDefaultAsync();

            List<Serie> series = await this.context.Series
                .Where(x => x.IdEjercicioHecho == idEjerHecho).ToListAsync();
            PartialEjercicioModel model = new PartialEjercicioModel();
            model.Ejercicio = ejer;
            model.Series = series;

            return model;
        }
        public async Task<List<PartialEjercicioModel>> GetEjerciciosHechosAsync(int idEntrenamiento)
        {
            List<PartialEjercicioModel> models = new List<PartialEjercicioModel>();
            List<EjercicioHecho> ejsHechos = await this.context.EjerciciosHechos
               .Where(x => x.Id == idEntrenamiento).ToListAsync();

            foreach (EjercicioHecho ejHecho in ejsHechos)
            {
                int idEjHecho = ejHecho.Id;
                PartialEjercicioModel model = await GetEjercicioEntrenamientoAsync(idEjHecho);
                model.EjercicioHecho = ejHecho;
                models.Add(model);
            }
            return models;
        }


        public async Task<List<VistaEntrenamiento>> GetFullEntrenamientosAsync(int idUsuario)
        {
            List<VistaEntrenamiento> vistas = new List<VistaEntrenamiento>();
            List<Entrenamiento> entrenamientos = await this.context.Entrenamientos
                .Where(x => x.IdUsuario == idUsuario).ToListAsync();
            foreach(Entrenamiento entrenamiento in entrenamientos)
            {
                VistaEntrenamiento vista = new VistaEntrenamiento();
                vista.Id = entrenamiento.Id;
                vista.EjerciciosHechosModel = await GetEjerciciosHechosAsync(entrenamiento.Id);
                vistas.Add(vista);
            }
            return vistas;
        }

        public async Task InsertFullEntrenamientoAsync(VistaEntrenamiento vista)
        {
            await this.context.Entrenamientos.AddAsync(vista.Entrenamiento);
            await this.context.SaveChangesAsync();

            foreach (PartialEjercicioModel ejsModel in vista.EjerciciosHechosModel)
            {
                await this.context.EjerciciosHechos.AddAsync(ejsModel.EjercicioHecho);
                await this.context.SaveChangesAsync();
                foreach (Serie serie in ejsModel.Series)
                {
                    await this.context.Series.AddAsync(serie);

                }
            }


            await this.context.SaveChangesAsync();
        }

        public async Task<int> GetMaxIdAsync()
        {
            if(await this.context.Entrenamientos.CountAsync() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Entrenamientos.MaxAsync(x => x.Id) + 1 ;
            }
        }

    }
}
