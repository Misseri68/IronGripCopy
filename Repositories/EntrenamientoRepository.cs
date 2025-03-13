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
        
        public async Task<List<PartialEjercicioModel>> GetEjerciciosHechosAsync(int idEntrenamiento)
        {
            List<PartialEjercicioModel> models = new List<PartialEjercicioModel>();
            List<EjercicioHecho> ejsHechos =  await this.context.EjerciciosHechos
               .Where(x => x.Id == idEntrenamiento).ToListAsync();

            foreach(EjercicioHecho ejHecho in ejsHechos)
            {
                int idEjHecho = ejHecho.Id;
                PartialEjercicioModel model = await GetEjercicioEntrenamientoAsync(idEjHecho);
                model.EjercicioHecho = ejHecho;
                models.Add(model);
            }

            return models;
        }

        public async Task<PartialEjercicioModel> GetEjercicioEntrenamientoAsync(int idEjerHecho)
        {
            Ejercicio ejer = await this.context.Ejercicios
                .Where(x => x.Id == idEjerHecho).FirstOrDefaultAsync();

            List<Serie> series = await this.context.Series
                .Where(x => x.IdEjercicioHecho == idEjerHecho).ToListAsync();
            List<EjercicioTag> tagsIds = await this.context.EjerciciosTags
                .Where(x => x.IdEjercicio == ejer.Id).ToListAsync();
            List<Tag> tags = new List<Tag>();

            PartialEjercicioModel model = new PartialEjercicioModel();
            model.Ejercicio = ejer;
            model.Series = series;

            return model;
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
