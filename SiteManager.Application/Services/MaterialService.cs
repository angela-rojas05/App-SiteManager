using SiteManager.Domain.Interfaces;
using SiteManager.Models;

namespace SiteManager.Application.Services
{
    public class MaterialService
    {
        private readonly IMaterialRepository _repository;

        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Material> ObtenerTodos() => _repository.ObtenerTodos();
        public Material? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Material material) => _repository.Agregar(material);
        public void Actualizar(Material material) => _repository.Actualizar(material);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}