using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfUsuarioRepository : IUsuarioRepository
    {
        private readonly SiteManagerContext _context;

        public EfUsuarioRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Usuario> ObtenerTodos() => _context.Usuarios.ToList();

        public Usuario? ObtenerPorId(int id) => _context.Usuarios.Find(id);

        public void Agregar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
        }

        public void Actualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();
            }
        }
    }
}