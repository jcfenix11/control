namespace InfinApp.Web.DTOs
{
    public class ControlActividadDto
    {
        public long Id { get; set; }
        public long ActividadesColaboradorId { get; set; }
        public string ActividadNombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int? Corresponde { get; set; }
    }
}
