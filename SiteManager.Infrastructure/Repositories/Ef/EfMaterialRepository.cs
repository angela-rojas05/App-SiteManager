using Microsoft.EntityFrameworkCore;
using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Data;

namespace SiteManager.Infrastructure.Repositories.Ef
{
    public class EfMaterialRepository : IMaterialRepository
    {
        private readonly SiteManagerContext _context;

        public EfMaterialRepository(SiteManagerContext context)
        {
            _context = context;
        }

        public IEnumerable<Material> ObtenerTodos() => _context.Materiales
            .Include(m => m.Siniestro)
            .ToList();

        public Material? ObtenerPorId(int id) => _context.Materiales
            .Include(m => m.Siniestro)
            .FirstOrDefault(m => m.Id == id);

        public void Agregar(Material material)
        {
            _context.Materiales.Add(material);
            _context.SaveChanges();
        }

        public void Actualizar(Material material)
        {
            _context.Materiales.Update(material);
            _context.SaveChanges();
        }

        public void Eliminar(int id)
        {
            var material = _context.Materiales.Find(id);
            if (material != null)
            {
                _context.Materiales.Remove(material);
                _context.SaveChanges();
            }
        }
    }
}