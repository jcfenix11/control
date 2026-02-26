using InfinApp.Web.DTOs;
using InfinApp.Web.Models;
using InfinApp.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfinApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ControlActividadApiController : ControllerBase
    {
        private readonly IControlActividadService _service;

        public ControlActividadApiController(IControlActividadService service)
        {
            _service = service;
        }

        // GET: api/ControlActividadApi/PorColaborador/1
        [HttpGet("porColaborador/{colaboradorId}")]
        public async Task<ActionResult<List<ControlActividadDto>>> PorColaborador(int colaboradorId)
        {
            var lista = await _service.ObtenerPorColaborador(colaboradorId);
            return Ok(lista);
        }

        // GET: api/ControlActividadApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ControlActividadDto>> GetById(int id)
        {
            var item = await _service.ObtenerPorId(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/ControlActividadApi
        [HttpPost]
        public async Task<ActionResult<int>> Crear(ControlActividad model)
        {
            var id = await _service.Crear(model);
            return Ok(id);
        }

        // PUT: api/ControlActividadApi/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, ControlActividad model)
        {
            if (id != model.Id) return BadRequest();
            var result = await _service.Actualizar(model);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/ControlActividadApi/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var result = await _service.Eliminar(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
