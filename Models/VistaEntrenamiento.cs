namespace IronGrip.Models
{
    public class VistaEntrenamiento
    {
        public VistaEntrenamiento()
        {
            this.Entrenamiento = new Entrenamiento();
            this.Entrenamiento.Fecha = DateTime.Now;
            this.SelectedTags = new List<int>();
        }

        public int Id
        {
            get => Entrenamiento.Id;
            set
            {
                
                Entrenamiento.Id = value;
            }
        }


        public Entrenamiento Entrenamiento { get; set; }
        public List<Tag> TagsDisponibles { get; set; }
        public List<int> SelectedTags { get; set; }
        public List<PartialEjercicioModel> EjerciciosHechosModel { get; set; }

    }
}
