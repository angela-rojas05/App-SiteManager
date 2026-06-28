using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
namespace SiteManager.Application.Services
{
    public class ReporteService
    {
        private readonly IReporteRepository _repository;

        public ReporteService(IReporteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Reporte> ObtenerTodos() => _repository.ObtenerTodos();
        public Reporte? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Reporte reporte) => _repository.Agregar(reporte);
        public void Actualizar(Reporte reporte) => _repository.Actualizar(reporte);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}