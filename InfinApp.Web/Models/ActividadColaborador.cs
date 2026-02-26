namespace InfinApp.Web.Models
{
    public class ActividadColaborador
    {
        public long Id { get; set; }

        public string? Actividad { get; set; }

        public string? Descripcion { get; set; }

        public bool? Estatus { get; set; }

        public int? Rol { get; set; }

        public DateTime FechaCracion { get; set; }
    }
}
