using SiteManager.Domain.Interfaces;
using SiteManager.Models;

namespace SiteManager.Application.Services
{
    public class EvidenciaService
    {
        private readonly IEvidenciaRepository _repository;

        public EvidenciaService(IEvidenciaRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Evidencia> ObtenerTodos() => _repository.ObtenerTodos();
        public Evidencia? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Evidencia evidencia) => _repository.Agregar(evidencia);
        public void Actualizar(Evidencia evidencia) => _repository.Actualizar(evidencia);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}