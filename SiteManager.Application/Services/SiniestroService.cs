using SiteManager.Domain.Interfaces;
using SiteManager.Models;

namespace SiteManager.Application.Services
{
    public class SiniestroService
    {
        private readonly ISiniestroRepository _repository;

        public SiniestroService(ISiniestroRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Siniestro> ObtenerTodos() => _repository.ObtenerTodos();
        public Siniestro? ObtenerPorId(int id) => _repository.ObtenerPorId(id);
        public void Agregar(Siniestro siniestro) => _repository.Agregar(siniestro);
        public void Actualizar(Siniestro siniestro) => _repository.Actualizar(siniestro);
        public void Eliminar(int id) => _repository.Eliminar(id);
    }
}