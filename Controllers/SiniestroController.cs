using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class SiniestroController : Controller
    {
        private readonly SiteManagerContext _context;

        public SiniestroController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var siniestros = _context.Siniestros.ToList();
            foreach (var s in siniestros)
            {
                s.Cliente = _context.FindCliente(s.ClienteId);
            }
            return View(siniestros);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var siniestro = _context.FindSiniestro(id.Value);
            if (siniestro == null)
                return NotFound();

            siniestro.Cliente = _context.FindCliente(siniestro.ClienteId);
            siniestro.Evidencias = _context.Evidencias.Where(e => e.SiniestroId == siniestro.Id).ToList();
            siniestro.Cotizaciones = _context.Cotizaciones.Where(c => c.SiniestroId == siniestro.Id).ToList();

            return View(siniestro);
        }

        public IActionResult Create()
        {
            ViewBag.Clientes = _context.Clientes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Siniestro siniestro)
        {
            ModelState.Remove("Cliente");
            if (ModelState.IsValid)
            {
                _context.Add(siniestro);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _context.Clientes.ToList();
            return View(siniestro);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var siniestro = _context.FindSiniestro(id.Value);
            if (siniestro == null)
                return NotFound();

            ViewBag.Clientes = _context.Clientes.ToList();
            return View(siniestro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Siniestro siniestro)
        {
            if (id != siniestro.Id)
                return NotFound();

            ModelState.Remove("Cliente");
            if (ModelState.IsValid)
            {
                _context.Update(siniestro);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Clientes = _context.Clientes.ToList();
            return View(siniestro);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var siniestro = _context.FindSiniestro(id.Value);
            if (siniestro == null)
                return NotFound();

            siniestro.Cliente = _context.FindCliente(siniestro.ClienteId);
            return View(siniestro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var siniestro = _context.FindSiniestro(id);
            if (siniestro != null)
            {
                _context.Remove(siniestro);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}