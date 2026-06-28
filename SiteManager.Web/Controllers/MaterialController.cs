using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Domain.Models;

namespace SiteManager.Web.Controllers
{
    public class MaterialController : Controller
    {
        private readonly MaterialService _service;
        private readonly SiniestroService _siniestroService;

        public MaterialController(MaterialService service, SiniestroService siniestroService)
        {
            _service = service;
            _siniestroService = siniestroService;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var material = _service.ObtenerPorId(id.Value);
            if (material == null) return NotFound();
            return View(material);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Material material)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Agregar(material);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(material);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var material = _service.ObtenerPorId(id.Value);
            if (material == null) return NotFound();
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Material material)
        {
            if (id != material.Id) return NotFound();
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _service.Actualizar(material);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(material);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var material = _service.ObtenerPorId(id.Value);
            if (material == null) return NotFound();
            return View(material);
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