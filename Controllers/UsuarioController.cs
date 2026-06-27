using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly SiteManagerContext _context;

        public UsuarioController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Usuarios.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = _context.FindUsuario(id.Value);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = _context.FindUsuario(id.Value);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(usuario);
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var usuario = _context.FindUsuario(id.Value);
            if (usuario == null)
                return NotFound();

            return View(usuario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var usuario = _context.FindUsuario(id);
            if (usuario != null)
            {
                _context.Remove(usuario);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}