using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class ClienteController : Controller
    {
        private readonly SiteManagerContext _context;

        public ClienteController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Clientes.ToList());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var cliente = _context.FindCliente(id.Value);
            if (cliente == null)
                return NotFound();

            cliente.Siniestros = _context.Siniestros.Where(s => s.ClienteId == cliente.Id).ToList();
            return View(cliente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cliente = _context.FindCliente(id.Value);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cliente cliente)
        {
            if (id != cliente.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(cliente);
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cliente = _context.FindCliente(id.Value);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cliente = _context.FindCliente(id);
            if (cliente != null)
            {
                _context.Remove(cliente);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}