using Microsoft.AspNetCore.Mvc;
using SiteManager.Application.Services;
using SiteManager.Models;

namespace SiteManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly SiniestroService _siniestroService;
        private readonly ClienteService _clienteService;
        private readonly CotizacionService _cotizacionService;
        private readonly EvidenciaService _evidenciaService;
        private readonly MaterialService _materialService;
        private readonly UsuarioService _usuarioService;
        private readonly ReporteService _reporteService;

        public HomeController(
            SiniestroService siniestroService,
            ClienteService clienteService,
            CotizacionService cotizacionService,
            EvidenciaService evidenciaService,
            MaterialService materialService,
            UsuarioService usuarioService,
            ReporteService reporteService)
        {
            _siniestroService = siniestroService;
            _clienteService = clienteService;
            _cotizacionService = cotizacionService;
            _evidenciaService = evidenciaService;
            _materialService = materialService;
            _usuarioService = usuarioService;
            _reporteService = reporteService;
        }

        public IActionResult Index()
        {
            var siniestros = _siniestroService.ObtenerTodos();
            var clientes = _clienteService.ObtenerTodos();
            var cotizaciones = _cotizacionService.ObtenerTodos();
            var evidencias = _evidenciaService.ObtenerTodos();
            var materiales = _materialService.ObtenerTodos();
            var usuarios = _usuarioService.ObtenerTodos();
            var reportes = _reporteService.ObtenerTodos();

            ViewBag.TotalSiniestros = siniestros.Count();
            ViewBag.TotalClientes = clientes.Count();
            ViewBag.TotalCotizaciones = cotizaciones.Count();
            ViewBag.TotalEvidencias = evidencias.Count();
            ViewBag.TotalMateriales = materiales.Count();
            ViewBag.TotalUsuarios = usuarios.Count();
            ViewBag.TotalReportes = reportes.Count();

            ViewBag.EnLevantamiento = siniestros.Count(s => s.Estado == EstadoSiniestro.Levantamiento);
            ViewBag.EnProceso = siniestros.Count(s => s.Estado == EstadoSiniestro.EnProceso);
            ViewBag.Completados = siniestros.Count(s =>
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