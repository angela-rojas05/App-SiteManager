using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.Application.Services
{
    public class CotizacionService
    {
        private readonly ICotizacionRepository _repository;

        public CotizacionService(ICotizacionRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Cotizacion> ObtenerTodos() => _repository.ObtenerTodos();
        public Cotizacion? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Cotizacion cotizacion) => _repository.Agregar(cotizacion);
        public void Actualizar(Cotizacion cotizacion) => _repository.Actualizar(cotizacion);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}