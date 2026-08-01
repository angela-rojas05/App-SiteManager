using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfReporteRepository : IReporteRepository
    {
        private readonly SiteManagerContext _context;

        public EfReporteRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Reporte> ObtenerTodos() => _context.Reportes
            .Include(r => r.Siniestro)
            .ToList();

        public Reporte? ObtenerPorId(int id) => _context.Reportes
            .Include(r => r.Siniestro)
            .FirstOrDefault(r => r.Id == id);

        public void Agregar(Reporte reporte)
        {
            _context.Reportes.Add(reporte);
            _context.SaveChanges();
        }

        public void Actualizar(Reporte reporte)
        {
            _context.Reportes.Update(reporte);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var reporte = _context.Reportes.Find(id);
            if (reporte != null)
            {
                _context.Reportes.Remove(reporte);
                _context.SaveChanges();
            }
        }
    }
}