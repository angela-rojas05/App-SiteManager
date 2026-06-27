using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class EvidenciaController : Controller
    {
        private readonly SiteManagerContext _context;

        public EvidenciaController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var evidencias = _context.Evidencias.ToList();
            foreach (var e in evidencias)
            {
                e.Siniestro = _context.FindSiniestro(e.SiniestroId);
            }
            return View(evidencias);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var evidencia = _context.FindEvidencia(id.Value);
            if (evidencia == null)
                return NotFound();

            evidencia.Siniestro = _context.FindSiniestro(evidencia.SiniestroId);
            return View(evidencia);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Evidencia evidencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(evidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(evidencia);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var evidencia = _context.FindEvidencia(id.Value);
            if (evidencia == null)
                return NotFound();

            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(evidencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Evidencia evidencia)
        {
            if (id != evidencia.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(evidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(evidencia);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var evidencia = _context.FindEvidencia(id.Value);
            if (evidencia == null)
                return NotFound();

            evidencia.Siniestro = _context.FindSiniestro(evidencia.SiniestroId);
            return View(evidencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var evidencia = _context.FindEvidencia(id);
            if (evidencia != null)
            {
                _context.Remove(evidencia);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}