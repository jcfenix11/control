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

        // PUT: api/ActividadColaboradorApi
        [HttpPut("actualizar")]
        public async Task<IActionResult> Actualizar([FromBody] ActividadColaborador model)
        {
            var resultado = await _service.Actualizar(model);

            if (!resultado)
                return BadRequest();

            return Ok();
        }

        // DELETE: api/ActividadColaboradorApi
        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Eliminar(long id)
        {
            var resultado = await _service.Eliminar(id);

            if (!resultado)
                return NotFound();

            return Ok();
        }
    }
}
