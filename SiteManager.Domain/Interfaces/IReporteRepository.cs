using SiteManager.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface IReporteRepository
    {
        IEnumerable<Reporte> ObtenerTodos();
        Reporte? ObtenerPorId(int id);
        void Agregar(Reporte reporte);
        void Actualizar(Reporte reporte);
        void Eliminar(int id);
    }
}