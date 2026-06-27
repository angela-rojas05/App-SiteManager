using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly SiteManagerContext _context;

        public CotizacionController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cotizaciones = _context.Cotizaciones.ToList();
            foreach (var c in cotizaciones)
            {
                c.Siniestro = _context.FindSiniestro(c.SiniestroId);
            }
            return View(cotizaciones);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var cotizacion = _context.FindCotizacion(id.Value);
            if (cotizacion == null)
                return NotFound();

            cotizacion.Siniestro = _context.FindSiniestro(cotizacion.SiniestroId);
            return View(cotizacion);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Cotizacion cotizacion)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _context.Add(cotizacion);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(cotizacion);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var cotizacion = _context.FindCotizacion(id.Value);
            if (cotizacion == null)
                return NotFound();

            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(cotizacion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Cotizacion cotizacion)
        {
            if (id != cotizacion.Id)
                return NotFound();

            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _context.Update(cotizacion);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(cotizacion);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var cotizacion = _context.FindCotizacion(id.Value);
            if (cotizacion == null)
                return NotFound();

            cotizacion.Siniestro = _context.FindSiniestro(cotizacion.SiniestroId);
            return View(cotizacion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var cotizacion = _context.FindCotizacion(id);
            if (cotizacion != null)
            {
                _context.Remove(cotizacion);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}