using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Models;

namespace SiteManager.Web.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly CotizacionService _service;
        private readonly SiniestroService _siniestroService;

        public CotizacionController(CotizacionService service, SiniestroService siniestroService)
        {
            _service = service;
            _siniestroService = siniestroService;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var cotizacion = _service.ObtenerPorId(id.Value);
            if (cotizacion == null) return NotFound();
            return View(cotizacion);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cotizacion cotizacion)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Agregar(cotizacion);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(cotizacion);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var cotizacion = _service.ObtenerPorId(id.Value);
            if (cotizacion == null) return NotFound();
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(cotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cotizacion cotizacion)
        {
            if (id != cotizacion.Id) return NotFound();
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Actualizar(cotizacion);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(cotizacion);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var cotizacion = _service.ObtenerPorId(id.Value);
            if (cotizacion == null) return NotFound();
            return View(cotizacion);
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