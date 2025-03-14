using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("ejercicio_hecho")]
    public class EjercicioHecho
    {
        [Key]
        [Column("id_ejercicio_hecho")]
        public int Id { get; set; }

        [Column("notas")]
        public string? Notas { get; set; }

        [Column("id_entrenamiento")] //FOREIGN KEY
        public int IdEntrenamiento { get; set; }

        [Column("id_ejercicio")] //FOREIGN KEY
        public int IdEjercicio { get; set; }
    }
}
