using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("entrenamiento_tag")]
    public class EntrenamientoTag
    {
        [Column("id_entrenamiento")]
        public int IdEntrenamiento { get; set; }


        [ Column("id_tag")]
        public int IdTag { get; set; }

    }
}
