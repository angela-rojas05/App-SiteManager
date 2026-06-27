using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class ReporteController : Controller
    {
        private readonly SiteManagerContext _context;

        public ReporteController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reportes = _context.Reportes.ToList();
            foreach (var r in reportes)
            {
                r.Siniestro = _context.FindSiniestro(r.SiniestroId);
            }
            return View(reportes);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var reporte = _context.FindReporte(id.Value);
            if (reporte == null)
                return NotFound();

            reporte.Siniestro = _context.FindSiniestro(reporte.SiniestroId);
            return View(reporte);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Reporte reporte)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _context.Add(reporte);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(reporte);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var reporte = _context.FindReporte(id.Value);
            if (reporte == null)
                return NotFound();

            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(reporte);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Reporte reporte)
        {
            if (id != reporte.Id)
                return NotFound();

            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _context.Update(reporte);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(reporte);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var reporte = _context.FindReporte(id.Value);
            if (reporte == null)
                return NotFound();

            reporte.Siniestro = _context.FindSiniestro(reporte.SiniestroId);
            return View(reporte);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var reporte = _context.FindReporte(id);
            if (reporte != null)
            {
                _context.Remove(reporte);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}