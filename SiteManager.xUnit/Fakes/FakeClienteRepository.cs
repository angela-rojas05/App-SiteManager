using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.xUnit.Fakes
{
    public class FakeClienteRepository : IClienteRepository
    {
        private readonly List<Cliente> _clientes = new();

        public IEnumerable<Cliente> ObtenerTodos() => _clientes;

        public Cliente? ObtenerPorId(int id) => _clientes.FirstOrDefault(c => c.Id == id);

        public void Agregar(Cliente cliente)
        {
            cliente.Id = _clientes.Count > 0 ? _clientes.Max(c => c.Id) + 1 : 1;
            _clientes.Add(cliente);
        }

        public void Actualizar(Cliente cliente)
        {
            var index = _clientes.FindIndex(c => c.Id == cliente.Id);
            if (index != -1) _clientes[index] = cliente;
        }

        public void Eliminar(int id) => _clientes.RemoveAll(c => c.Id == id);
    }
}