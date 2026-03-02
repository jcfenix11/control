using InfinApp.Web.Models;

namespace InfinApp.Web.Repositories
{
    public interface IActividadColaboradorRepository
    {
        Task<List<ActividadColaborador>> ObtenerTodos();
        Task<ActividadColaborador?> ObtenerPorId(long id);
        Task<ActividadColaborador?> Crear(ActividadColaborador model);
        Task<ActividadColaborador?> Actualizar(ActividadColaborador model);
        Task<bool> Eliminar(long id);
    }
}
