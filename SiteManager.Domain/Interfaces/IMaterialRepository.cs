using SiteManager.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface IMaterialRepository
    {
        IEnumerable<Material> ObtenerTodos();
        Material? ObtenerPorId(int id);
        void Agregar(Material material);
        void Actualizar(Material material);
        void Eliminar(int id);
    }
}