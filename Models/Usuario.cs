using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int Id { get; set; }


        [Column("nombre")]
        public string Nombre { get; set; }


        [Column("correo")]
        public string Email { get; set; }

        [Column("foto")]
        public string Imagen { get; set; }


        [Column("pwd")]
        public byte[] Password { get; set; }


        [Column("salt")]
        public string Salt { get; set; }
    }
}
