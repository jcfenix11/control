using InfinApp.Web.Models;
using InfinApp.Web.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InfinApp.Web.Controllers
{
    public class ControlActividadController : Controller
    {
        private readonly IControlActividadService _service;
        private readonly IActividadColaboradorService _actividadService;

        public ControlActividadController(
            IControlActividadService service,
            IActividadColaboradorService actividadService)
        {
            _service = service;
            _actividadService = actividadService;
        }

        // GET: ControlActividad
        public async Task<IActionResult> Index()
        {
            var lista = await _service.ObtenerTodos();
            ViewBag.ActividadColaborador = await _actividadService.ObtenerTodos(); // Para select en Razor
            Console.WriteLine("numero de objetos: " + lista.Count);
            return View(lista);
        }

        // GET: ControlActividad/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var control = await _service.ObtenerPorId(id);
            if (control == null) return NotFound();
            return View(control);
        }

        // GET: ControlActividad/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.ActividadesColaborador = await _actividadService.ObtenerTodos();
            return View();
        }

        // POST: ControlActividad/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ControlActividad model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ActividadesColaborador = await _actividadService.ObtenerTodos();
                return View(model);
            }

            await _service.Crear(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: ControlActividad/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var control = await _service.ObtenerPorId(id);
            if (control == null) return NotFound();

            ViewBag.ActividadesColaborador = await _actividadService.ObtenerTodos();
            return View(control);
        }

        // POST: ControlActividad/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ControlActividad model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ActividadesColaborador = await _actividadService.ObtenerTodos();
                return View(model);
            }

            await _service.Actualizar(model);
            return RedirectToAction(nameof(Index));
        }

        // GET: ControlActividad/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var control = await _service.ObtenerPorId(id);
            if (control == null) return NotFound();
            return View(control);
        }

        // POST: ControlActividad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
