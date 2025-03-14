using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IronGrip.Models
{
    [Table("serie")]
    public class Serie
    {

        public Serie()
        {
            this.NumSeries = 1;
            this.Peso = 10;
            this.Repeticiones = 10;
        }

        [Key]
        [Column("id_serie")]
        public int Id { get; set; }

        [Column("peso")]
        public float Peso { get; set; }

        [Column("repeticiones")]
        public int Repeticiones { get; set; }

        [Column("tiempo")]
        public int? TiempoMins { get; set; }

        [Column("num_series")]
        public int NumSeries { get; set; }

        [Column("id_ejercicio_hecho")] //FOREIGN KEY
        public int IdEjercicioHecho { get; set; }
    }
}
