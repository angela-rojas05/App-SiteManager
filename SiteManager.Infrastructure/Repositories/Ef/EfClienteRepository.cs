using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfClienteRepository : IClienteRepository
    {
        private readonly SiteManagerContext _context;

        public EfClienteRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Cliente> ObtenerTodos() => _context.Clientes.ToList();

        public Cliente? ObtenerPorId(int id) => _context.Clientes
            .Include(c => c.Siniestros)
            .FirstOrDefault(c => c.Id == id);

        public void Agregar(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            _context.SaveChanges();
        }

        public void Actualizar(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }
    }
}