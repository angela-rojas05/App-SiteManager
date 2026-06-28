using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Domain.Models;

namespace SiteManager.Web.Controllers
{
    public class ReporteController : Controller
    {
        private readonly ReporteService _service;
        private readonly SiniestroService _siniestroService;

        public ReporteController(ReporteService service, SiniestroService siniestroService)
        {
            _service = service;
            _siniestroService = siniestroService;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var reporte = _service.ObtenerPorId(id.Value);
            if (reporte == null) return NotFound();
            return View(reporte);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reporte reporte)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Agregar(reporte);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(reporte);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var reporte = _service.ObtenerPorId(id.Value);
            if (reporte == null) return NotFound();
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(reporte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reporte reporte)
        {
            if (id != reporte.Id) return NotFound();
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Actualizar(reporte);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(reporte);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var reporte = _service.ObtenerPorId(id.Value);
            if (reporte == null) return NotFound();
            return View(reporte);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _service.Eliminar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}