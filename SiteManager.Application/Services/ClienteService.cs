using SiteManager.Domain.Interfaces;
using SiteManager.Models;

namespace SiteManager.Application.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Cliente> ObtenerTodos() => _repository.ObtenerTodos();
        public Cliente? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Cliente cliente) => _repository.Agregar(cliente);
        public void Actualizar(Cliente cliente) => _repository.Actualizar(cliente);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}