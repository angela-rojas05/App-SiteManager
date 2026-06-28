using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Domain.Models;

namespace SiteManager.Web.Controllers
{
    public class EvidenciaController : Controller
    {
        private readonly EvidenciaService _service;
        private readonly SiniestroService _siniestroService;

        public EvidenciaController(EvidenciaService service, SiniestroService siniestroService)
        {
            _service = service;
            _siniestroService = siniestroService;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var evidencia = _service.ObtenerPorId(id.Value);
            if (evidencia == null) return NotFound();
            return View(evidencia);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Evidencia evidencia)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Agregar(evidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(evidencia);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var evidencia = _service.ObtenerPorId(id.Value);
            if (evidencia == null) return NotFound();
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(evidencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Evidencia evidencia)
        {
            if (id != evidencia.Id) return NotFound();
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Actualizar(evidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(evidencia);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var evidencia = _service.ObtenerPorId(id.Value);
            if (evidencia == null) return NotFound();
            return View(evidencia);
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