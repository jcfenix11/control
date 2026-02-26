using InfinApp.Web.Models;
using InfinApp.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace InfinApp.Web.Controllers
{
    public class ActividadColaboradorController : Controller
    {
        private readonly IActividadColaboradorService _service;

        public ActividadColaboradorController(IActividadColaboradorService service)
        {
            _service = service;
        }

        // GET: ActividadColaborador
        public async Task<IActionResult> Index()
        {
            var lista = await _service.ObtenerTodos();
            return View(lista);
        }

        // GET: ActividadColaborador/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var actividad = await _service.ObtenerPorId(id);
            if (actividad == null) return NotFound();
            return View(actividad);
        }

        // GET: ActividadColaborador/Create
        public IActionResult Create() => View();

        // POST: ActividadColaborador/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ActividadColaborador model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.Crear(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: ActividadColaborador/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var actividad = await _service.ObtenerPorId(id);
            if (actividad == null) return NotFound();
            return View(actividad);
        }

        // POST: ActividadColaborador/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ActividadColaborador model)
        {
            if (!ModelState.IsValid) return View(model);
            await _service.Actualizar(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: ActividadColaborador/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var actividad = await _service.ObtenerPorId(id);
            if (actividad == null) return NotFound();
            return View(actividad);
        }

        // POST: ActividadColaborador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
