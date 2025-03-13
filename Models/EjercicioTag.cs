using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{

    [Table("ejercicio_tag")]
    public class EjercicioTag
    {
        [Column("id_ejercicio")]
        public int IdEjercicio { get; set; }

        [Column("id_tag")]
        public int IdTag { get; set; }
    }

}
