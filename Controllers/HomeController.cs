using Microsoft.AspNetCore.Mvc;
using SiteManager.Data;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiteManagerContext _context;

        public HomeController(SiteManagerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalSiniestros = _context.Siniestros.Count;
            ViewBag.TotalClientes = _context.Clientes.Count;
            ViewBag.TotalCotizaciones = _context.Cotizaciones.Count;
            ViewBag.TotalEvidencias = _context.Evidencias.Count;
            ViewBag.TotalMateriales = _context.Materiales.Count;
            ViewBag.TotalUsuarios = _context.Usuarios.Count;
            ViewBag.TotalReportes = _context.Reportes.Count;


            ViewBag.EnLevantamiento = _context.Siniestros.Count(s => s.Estado == EstadoSiniestro.Levantamiento);
            ViewBag.EnProceso = _context.Siniestros.Count(s => s.Estado == EstadoSiniestro.EnProceso);
            ViewBag.Completados = _context.Siniestros.Count(s =>
                s.Estado == EstadoSiniestro.Finalizado ||
                s.Estado == EstadoSiniestro.Cerrado);


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}