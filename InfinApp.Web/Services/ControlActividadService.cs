using InfinApp.Web.DTOs;
using InfinApp.Web.Models;
using InfinApp.Web.Repositories;

namespace InfinApp.Web.Services
{
    public class ControlActividadService : IControlActividadService
    {
        private readonly IControlActividadRepository _repository;
        private readonly IActividadColaboradorRepository _actividadRepo;

        public ControlActividadService(
            IControlActividadRepository repository,
            IActividadColaboradorRepository actividadRepo)
        {
            _repository = repository;
            _actividadRepo = actividadRepo;
        }

        public Task<int> Crear(ControlActividad model)
        {
            return _repository.Crear(model);
        }

        public Task<bool> Actualizar(ControlActividad model)
        {
            return _repository.Actualizar(model);
        }

        public Task<bool> Eliminar(int id)
        {
            return _repository.Eliminar(id);
        }

        public Task<List<ControlActividad>> ObtenerTodos() => _repository.ObtenerPorColaborador(null);

        // Devuelve DTOs listos para AJAX
        public async Task<List<ControlActividadDto>> ObtenerPorColaborador(int? colaboradorId)
        {
            var actividades = await _repository.ObtenerPorColaborador(colaboradorId);
            var colaboradores = await _actividadRepo.ObtenerTodos();

            return actividades.Select(a =>
            {
                var colab = colaboradores.FirstOrDefault(c => c.Id == a.ActividadesColaboradorId);
                return new ControlActividadDto
                {
                    Id = a.Id,
                    ActividadesColaboradorId = a.ActividadesColaboradorId,
                    ActividadNombre = colab?.Actividad ?? string.Empty,
                    Descripcion = colab?.Descripcion,
                    FechaInicio = a.FechaInicio,
                    FechaFin = a.FechaFin,
                    Corresponde = a.Corresponde
                };
            }).ToList();
        }

        public Task<ControlActividad?> ObtenerPorId(int id)
        {
            return _repository.ObtenerPorId(id);
        }
    }
}
