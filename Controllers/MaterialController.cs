using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class MaterialController : Controller
    {
        private readonly SiteManagerContext _context;

        public MaterialController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var materiales = _context.Materiales.ToList();
            foreach (var m in materiales)
            {
                m.Siniestro = m.SiniestroId.HasValue ? _context.FindSiniestro(m.SiniestroId.Value) : null;
            }
            return View(materiales);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();

            var material = _context.FindMaterial(id.Value);
            if (material == null)
                return NotFound();

            material.Siniestro = material.SiniestroId.HasValue ? _context.FindSiniestro(material.SiniestroId.Value) : null;
            return View(material);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Material material)
        {
            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _context.Add(material);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(material);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var material = _context.FindMaterial(id.Value);
            if (material == null)
                return NotFound();

            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Material material)
        {
            if (id != material.Id)
                return NotFound();

            ModelState.Remove("Siniestro");
            if (ModelState.IsValid)
            {
                _context.Update(material);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _context.Siniestros.ToList();
            return View(material);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var material = _context.FindMaterial(id.Value);
            if (material == null)
                return NotFound();

            material.Siniestro = material.SiniestroId.HasValue ? _context.FindSiniestro(material.SiniestroId.Value) : null;
            return View(material);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var material = _context.FindMaterial(id);
            if (material != null)
            {
                _context.Remove(material);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}