using SiteManager.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface ISiniestroRepository
    {
        IEnumerable<Siniestro> ObtenerTodos();
        Siniestro? ObtenerPorId(int id);
        void Agregar(Siniestro siniestro);
        void Actualizar(Siniestro siniestro);
        void Eliminar(int id);
    }
}