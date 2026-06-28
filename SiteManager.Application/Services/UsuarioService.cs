using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.Application.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Usuario> ObtenerTodos() => _repository.ObtenerTodos();
        public Usuario? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Usuario usuario) => _repository.Agregar(usuario);
        public void Actualizar(Usuario usuario) => _repository.Actualizar(usuario);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}