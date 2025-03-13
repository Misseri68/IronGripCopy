using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("ejercicio")]
    public class Ejercicio
    {
        [Key]
        [Column("id_ejercicio")]
        public int Id { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("descripcion")]
        public string Descripcion { get; set; }

        [Column("foto")]
        public string Foto { get; set; }

        [Column("es_tiempo")]
        public bool EsTiempo { get; set; }

        [Column("id_usuario")] //FOREIGN KEY
        public int IdUsuario { get; set; }

        public List<Tag>? Tags { get; set; }
    }
}
