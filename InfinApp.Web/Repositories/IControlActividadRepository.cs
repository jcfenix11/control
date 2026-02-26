using InfinApp.Web.Models;

namespace InfinApp.Web.Repositories
{
    public interface IControlActividadRepository
    {
        Task<List<ControlActividad>> ObtenerTodos();

        Task<ControlActividad?> ObtenerPorId(int id);

        Task<int> Crear(ControlActividad model);

        Task<bool> Actualizar(ControlActividad model);

        Task<bool> Eliminar(int id);

        Task<List<ControlActividad>> ObtenerPorColaborador(int? colaboradorId);
    }
}
