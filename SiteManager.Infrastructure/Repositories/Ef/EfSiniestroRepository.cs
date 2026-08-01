using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfSiniestroRepository : ISiniestroRepository
    {
        private readonly SiteManagerContext _context;

        public EfSiniestroRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Siniestro> ObtenerTodos() => _context.Siniestros
            .Include(s => s.Cliente)
            .ToList();

        public Siniestro? ObtenerPorId(int id) => _context.Siniestros
            .Include(s => s.Cliente)
            .Include(s => s.Evidencias)
            .Include(s => s.Cotizaciones)
            .FirstOrDefault(s => s.Id == id);

        public void Agregar(Siniestro siniestro)
        {
            _context.Siniestros.Add(siniestro);
            _context.SaveChanges();
        }

        public void Actualizar(Siniestro siniestro)
        {
            _context.Siniestros.Update(siniestro);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var siniestro = _context.Siniestros.Find(id);
            if (siniestro != null)
            {
                _context.Siniestros.Remove(siniestro);
                _context.SaveChanges();
            }
        }
    }
}