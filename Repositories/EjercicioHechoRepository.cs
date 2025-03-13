using IronGrip.Data;
using Microsoft.EntityFrameworkCore;

namespace IronGrip.Repositories
{
    public class EjercicioHechoRepository
    {
        private IronGripContext context;
        public EjercicioHechoRepository(IronGripContext context) 
        {
            this.context = context;
        }


        public async Task<int> GetMaxIdAsync()
        {
            if(this.context.EjerciciosHechos.Count()== 0)
            {
                return 1;
            }
            else
            {
                return await this.context.EjerciciosHechos.MaxAsync(x => x.Id) + 1;
            }
        }

        public async Task<int> GetMaxIdSerieAsync()
        {
            if (this.context.Series.Count() == 0)
            {
                return 1;
            }
            else
            {
                return await this.context.Series.MaxAsync(x => x.Id) + 1;
            }
        }
    }
}
