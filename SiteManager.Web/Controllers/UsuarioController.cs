using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Models;

namespace SiteManager.Web.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var usuario = _service.ObtenerPorId(id.Value);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _service.Agregar(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var usuario = _service.ObtenerPorId(id.Value);
            if (usuario == null) return NotFound();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id) return NotFound();
            if (ModelState.IsValid)
            {
                _service.Actualizar(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var usuario = _service.ObtenerPorId(id.Value);
            if (usuario == null) return NotFound();
            return View(usuario);
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