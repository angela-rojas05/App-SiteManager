using SiteManager.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface IEvidenciaRepository
    {
        IEnumerable<Evidencia> ObtenerTodos();
        Evidencia? ObtenerPorId(int id);
        void Agregar(Evidencia evidencia);
        void Actualizar(Evidencia evidencia);
        void Eliminar(int id);
    }
}