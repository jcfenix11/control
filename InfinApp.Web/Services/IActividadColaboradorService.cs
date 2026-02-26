using InfinApp.Web.Models;

namespace InfinApp.Web.Services
{
    public interface IActividadColaboradorService
    {
        Task<List<ActividadColaborador>> ObtenerTodos();
        Task<ActividadColaborador?> ObtenerPorId(int id);
        Task<int> Crear(ActividadColaborador model);
        Task<bool> Actualizar(ActividadColaborador model);
        Task<bool> Eliminar(int id);
    }
}
