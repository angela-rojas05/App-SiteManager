using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfEvidenciaRepository : IEvidenciaRepository
    {
        private readonly SiteManagerContext _context;

        public EfEvidenciaRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Evidencia> ObtenerTodos() => _context.Evidencias
            .Include(e => e.Siniestro)
            .ToList();

        public Evidencia? ObtenerPorId(int id) => _context.Evidencias
            .Include(e => e.Siniestro)
            .FirstOrDefault(e => e.Id == id);

        public void Agregar(Evidencia evidencia)
        {
            _context.Evidencias.Add(evidencia);
            _context.SaveChanges();
        }

        public void Actualizar(Evidencia evidencia)
        {
            _context.Evidencias.Update(evidencia);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var evidencia = _context.Evidencias.Find(id);
            if (evidencia != null)
            {
                _context.Evidencias.Remove(evidencia);
                _context.SaveChanges();
            }
        }
    }
}