using InfinApp.Web.DTOs;
using InfinApp.Web.Models;

namespace InfinApp.Web.Services
{
    public interface IControlActividadService
    {
        Task<List<ControlActividad>> ObtenerTodos();
        Task<ControlActividad?> ObtenerPorId(int id);
        Task<int> Crear(ControlActividad model);
        Task<bool> Actualizar(ControlActividad model);
        Task<bool> Eliminar(int id);

        // Método para obtener ControlActividad con información de ActividadColaborador
        Task<List<ControlActividadDto>> ObtenerPorColaborador(int? colaboradorId);
    }
}
