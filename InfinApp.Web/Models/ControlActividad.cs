namespace InfinApp.Web.Models
{

    public class ControlActividad
    {
        public long Id { get; set; }

        public long ActividadesColaboradorId { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public int? Corresponde { get; set; }
    }
}
