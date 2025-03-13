namespace IronGrip.Models
{
    public class PartialEjercicioModel
    {
        public EjercicioHecho EjercicioHecho { get; set; }

        public Ejercicio Ejercicio { get; set; }

        public List<Serie> Series { get; set; }

        public int NumTotalSeries()
        {
            int numSeries = 0;
            foreach(Serie s in Series)
            {
                numSeries += s.NumSeries;
            }
            return numSeries;
        }

        public int MediaKilos()
        {
            int kilos = 0;
            foreach (Serie s in Series) {
                kilos += ((int)s.Peso) * s.NumSeries ; 
            }
            int numSeries = NumTotalSeries();
            return kilos / numSeries;
        }

        public int NumTotalRepes()
        {
            int numRepes = 0;
            foreach(Serie s in Series)
            {
                numRepes += s.Repeticiones * s.NumSeries;
            }
            return numRepes;
        }
    }
}
