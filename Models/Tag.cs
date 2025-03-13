using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("tag")]
    public class Tag
    {
        [Key]
        [Column("id_tag")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("color")]
        public string Color { get; set; }

        [Column("id_usuario")] //FOREIGN KEY
        public int IdUsuario { get; set; }
    }
}
