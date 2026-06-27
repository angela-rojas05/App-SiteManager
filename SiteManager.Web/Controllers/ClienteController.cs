using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Models;

namespace SiteManager.Web.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ClienteService _service;

        public ClienteController(ClienteService service)
        {
            _service = service;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var cliente = _service.ObtenerPorId(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _service.Agregar(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var cliente = _service.ObtenerPorId(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _service.Actualizar(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var cliente = _service.ObtenerPorId(id.Value);
            if (cliente == null) return NotFound();
            return View(cliente);
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