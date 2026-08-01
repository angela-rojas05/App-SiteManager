using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfCotizacionRepository : ICotizacionRepository
    {
        private readonly SiteManagerContext _context;

        public EfCotizacionRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Cotizacion> ObtenerTodos() => _context.Cotizaciones
            .Include(c => c.Siniestro)
            .ToList();

        public Cotizacion? ObtenerPorId(int id) => _context.Cotizaciones
            .Include(c => c.Siniestro)
            .FirstOrDefault(c => c.Id == id);

        public void Agregar(Cotizacion cotizacion)
        {
            _context.Cotizaciones.Add(cotizacion);
            _context.SaveChanges();
        }

        public void Actualizar(Cotizacion cotizacion)
        {
            _context.Cotizaciones.Update(cotizacion);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var cotizacion = _context.Cotizaciones.Find(id);
            if (cotizacion != null)
            {
                _context.Cotizaciones.Remove(cotizacion);
                _context.SaveChanges();
            }
        }
    }
}