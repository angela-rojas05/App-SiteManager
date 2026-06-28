using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Domain.Models;
namespace SiteManager.Web.Controllers
{
    public class SiniestroController : Controller
    {
        private readonly SiniestroService _service;
        private readonly ClienteService _clienteService;

        public SiniestroController(SiniestroService service, ClienteService clienteService)
        {
            _service = service;
            _clienteService = clienteService;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var siniestro = _service.ObtenerPorId(id.Value);
            if (siniestro == null) return NotFound();
            return View(siniestro);
        }

        public IActionResult Create()
        {
            ViewBag.Clientes = _clienteService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Siniestro siniestro)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Evidencias");
            ModelState.Remove("Cotizaciones");
            if (ModelState.IsValid)
            {
                _service.Agregar(siniestro);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _clienteService.ObtenerTodos();
            return View(siniestro);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var siniestro = _service.ObtenerPorId(id.Value);
            if (siniestro == null) return NotFound();
            ViewBag.Clientes = _clienteService.ObtenerTodos();
            return View(siniestro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Siniestro siniestro)
        {
            if (id != siniestro.Id) return NotFound();
            ModelState.Remove("Cliente");
            ModelState.Remove("Evidencias");
            ModelState.Remove("Cotizaciones");
            if (ModelState.IsValid)
            {
                _service.Actualizar(siniestro);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _clienteService.ObtenerTodos();
            return View(siniestro);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var siniestro = _service.ObtenerPorId(id.Value);
            if (siniestro == null) return NotFound();
            return View(siniestro);
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