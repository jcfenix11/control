using InfinApp.Web.Models;
using InfinApp.Web.Repositories;

namespace InfinApp.Web.Services
{
    public class ActividadColaboradorService : IActividadColaboradorService
    {
        private readonly IActividadColaboradorRepository _repository;

        public ActividadColaboradorService(IActividadColaboradorRepository repository)
        {
            _repository = repository;
        }

        public Task<ActividadColaborador?> Crear(ActividadColaborador model)
        {
            return _repository.Crear(model);
        }

        public Task<ActividadColaborador?> Actualizar(ActividadColaborador model)
        {
            return _repository.Actualizar(model);
        }

        public Task<bool> Eliminar(long id)
        {
            return _repository.Eliminar(id);
        }

        public Task<List<ActividadColaborador>> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

        public Task<ActividadColaborador?> ObtenerPorId(long id)
        {
            return _repository.ObtenerPorId(id);
        }
    }
}
