using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Domain.Models;

namespace SiteManager.Web.Controllers
{
    public class EvidenciaController : Controller
    {
        private readonly EvidenciaService _service;
        private readonly SiniestroService _siniestroService;
        private readonly IWebHostEnvironment _env;

        // Carpeta relativa donde se guardan las imágenes de evidencias (ADR-08)
        private const string CarpetaEvidencias = "uploads/evidencias";
        private static readonly string[] ExtensionesPermitidas = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long TamanoMaximoBytes = 5 * 1024 * 1024; // 5 MB

        public EvidenciaController(EvidenciaService service, SiniestroService siniestroService, IWebHostEnvironment env)
        {
            _service = service;
            _siniestroService = siniestroService;
            _env = env;
        }

        public IActionResult Index() => View(_service.ObtenerTodos());

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();
            var evidencia = _service.ObtenerPorId(id.Value);
            if (evidencia == null) return NotFound();
            return View(evidencia);
        }

        public IActionResult Create()
        {
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Evidencia evidencia, IFormFile? imagenFile)
        {
            ModelState.Remove("Siniestro");
            ModelState.Remove("RutaArchivo");

            ValidarImagen(imagenFile, requerida: true);

            if (ModelState.IsValid)
            {
                evidencia.RutaArchivo = await GuardarImagenAsync(imagenFile!);
                _service.Agregar(evidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(evidencia);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();
            var evidencia = _service.ObtenerPorId(id.Value);
            if (evidencia == null) return NotFound();
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(evidencia);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Evidencia evidencia, IFormFile? imagenFile)
        {
            if (id != evidencia.Id) return NotFound();
            ModelState.Remove("Siniestro");
            ModelState.Remove("RutaArchivo");

            // La imagen es opcional al editar: si no se sube una nueva, se conserva la existente
            ValidarImagen(imagenFile, requerida: false);

            if (ModelState.IsValid)
            {
                var existente = _service.ObtenerPorId(id);
                if (existente == null) return NotFound();

                if (imagenFile != null && imagenFile.Length > 0)
                {
                    var rutaAnterior = existente.RutaArchivo;
                    evidencia.RutaArchivo = await GuardarImagenAsync(imagenFile);
                    EliminarArchivoFisico(rutaAnterior);
                }
                else
                {
                    evidencia.RutaArchivo = existente.RutaArchivo;
                }

                _service.Actualizar(evidencia);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Siniestros = _siniestroService.ObtenerTodos();
            return View(evidencia);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();
            var evidencia = _service.ObtenerPorId(id.Value);
            if (evidencia == null) return NotFound();
            return View(evidencia);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var evidencia = _service.ObtenerPorId(id);
            _service.Eliminar(id);
            if (evidencia != null)
            {
                EliminarArchivoFisico(evidencia.RutaArchivo);
            }
            return RedirectToAction(nameof(Index));
        }

        // ---- Helpers privados para el manejo de archivos (ADR-08) ----

        private void ValidarImagen(IFormFile? imagenFile, bool requerida)
        {
            if (imagenFile == null || imagenFile.Length == 0)
            {
                if (requerida)
                {
                    ModelState.AddModelError("imagenFile", "Debe seleccionar una imagen.");
                }
                return;
            }

            var extension = Path.GetExtension(imagenFile.FileName).ToLowerInvariant();
            if (!ExtensionesPermitidas.Contains(extension))
            {
                ModelState.AddModelError("imagenFile", "Formato no permitido. Use JPG, PNG o WEBP.");
            }

            if (imagenFile.Length > TamanoMaximoBytes)
            {
                ModelState.AddModelError("imagenFile", "La imagen no debe superar los 5 MB.");
            }
        }

        private async Task<string> GuardarImagenAsync(IFormFile imagenFile)
        {
            var carpetaFisica = Path.Combine(_env.WebRootPath, CarpetaEvidencias);
            Directory.CreateDirectory(carpetaFisica);

            var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(imagenFile.FileName).ToLowerInvariant()}";
            var rutaFisica = Path.Combine(carpetaFisica, nombreArchivo);

            using (var stream = new FileStream(rutaFisica, FileMode.Create))
            {
                await imagenFile.CopyToAsync(stream);
            }

            // Ruta relativa que se guarda en la base de datos y se usa como src="" en las vistas
            return $"/{CarpetaEvidencias}/{nombreArchivo}";
        }

        private void EliminarArchivoFisico(string? rutaRelativa)
        {
            if (string.IsNullOrWhiteSpace(rutaRelativa)) return;

            var rutaFisica = Path.Combine(_env.WebRootPath, rutaRelativa.TrimStart('/'));
            if (System.IO.File.Exists(rutaFisica))
            {
                System.IO.File.Delete(rutaFisica);
            }
        }
    }
}