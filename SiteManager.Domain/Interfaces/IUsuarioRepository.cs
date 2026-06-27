using SiteManager.Models;

namespace SiteManager.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> ObtenerTodos();
        Usuario? ObtenerPorId(int id);
        void Agregar(Usuario usuario);
        void Actualizar(Usuario usuario);
        void Eliminar(int id);
    }
}