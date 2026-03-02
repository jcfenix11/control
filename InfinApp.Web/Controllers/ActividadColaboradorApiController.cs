using InfinApp.Web.DTOs;
using InfinApp.Web.Models;
using InfinApp.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfinApp.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadColaboradorApiController : ControllerBase
    {
        private readonly IActividadColaboradorService _service;

        public ActividadColaboradorApiController(IActividadColaboradorService service)
        {
            _service = service;
        }

        // GET: api/ActividadColaboradorApi/ObtenerTodos
        [HttpGet("obtenerTodos")]
        public async Task<ActionResult<List<ActividadColaborador>>> ObtenerTodos()
        {
            var lista = await _service.ObtenerTodos();
            return Ok(lista);
        }

        // GET: api/ActividadColaboradorApi/ObtenerPorId
        [HttpGet("obtenerPorId/{id}")]
        public async Task<ActionResult<ActividadColaborador>> ObtenerPorId(long id)
        {
            var item = await _service.ObtenerPorId(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/ActividadColaboradorApi
        [HttpPost("crear")]
        public async Task<IActionResult> Crear([FromBody] ActividadColaborador model)
        {
            var result = await _service.Crear(model);

            if (result == null)
                return BadRequest("No se pudo crear la actividad");

            return Ok(result);
        }

        // PUT: api/ActividadColaboradorApi
        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] ActividadColaborador model)
        {
            var result = await _service.Actualizar(model);

            if (result != null)
                return Ok(result);

            return NotFound("No se pudo actualizar");
        }

        // DELETE: api/ActividadColaboradorApi
        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var resultado = await _service.Eliminar(id);

            if (!resultado)
                return NotFound();

            return Ok();
        }
    }
}
