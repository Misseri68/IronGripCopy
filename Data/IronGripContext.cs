using IronGrip.Models;
using Microsoft.EntityFrameworkCore;

namespace IronGrip.Data
{
    public class IronGripContext : DbContext
    {
        public IronGripContext(DbContextOptions<IronGripContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Entrenamiento> Entrenamientos { get; set; }
        public DbSet<Ejercicio> Ejercicios { get; set; }
        public DbSet<EjercicioHecho> EjerciciosHechos { get; set; }
        public DbSet<Serie> Series { get; set; }


        public DbSet<EjercicioTag> EjerciciosTags { get; set; }
        public DbSet<EntrenamientoTag> EntrenamientosTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EjercicioTag>()
                .HasKey(eh => new { eh.IdEjercicio, eh.IdTag });

            modelBuilder.Entity<EntrenamientoTag>()
                .HasKey(et => new { et.IdEntrenamiento, et.IdTag });

        }
    }
}
