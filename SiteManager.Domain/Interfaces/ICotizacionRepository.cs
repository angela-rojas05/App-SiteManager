using SiteManager.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface ICotizacionRepository
    {
        IEnumerable<Cotizacion> ObtenerTodos();
        Cotizacion? ObtenerPorId(int id);
        void Agregar(Cotizacion cotizacion);
        void Actualizar(Cotizacion cotizacion);
        void Eliminar(int id);
    }
}