using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("entrenamiento")]
    public class Entrenamiento
    {
        [Key]
        [Column("id_entrenamiento")]
        public int Id { get; set; }


        [Column("fecha")]
        public DateTime Fecha { get; set; }


        [Column("notas")]
        public string? Notas { get; set; }


        [Column("id_usuario")] //FOREIGN KEY
        public int IdUsuario { get; set; }
    }
}
