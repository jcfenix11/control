using InfinApp.Web.Models;

namespace InfinApp.Web.Services
{
    public interface IActividadColaboradorService
    {
        Task<List<ActividadColaborador>> ObtenerTodos();
        Task<ActividadColaborador?> ObtenerPorId(long id);
        Task<ActividadColaborador?> Crear(ActividadColaborador model);
        Task<ActividadColaborador?> Actualizar(ActividadColaborador model);
        Task<bool> Eliminar(long id);
    }
}
