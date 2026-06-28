using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.Application.Services
{
    public class SiniestroService
    {
        private readonly ISiniestroRepository _repository;
        private readonly IEnumerable<ISiniestroObserver> _observers;

        public SiniestroService(ISiniestroRepository repository, IEnumerable<ISiniestroObserver> observers)
        {
            _repository = repository;
            _observers = observers;
        }

        public IEnumerable<Siniestro> ObtenerTodos() => _repository.ObtenerTodos();
        public Siniestro? ObtenerPorId(int id) => _repository.ObtenerPorId(id);

        public void Agregar(Siniestro siniestro) => _repository.Agregar(siniestro);

        public void Actualizar(Siniestro siniestro)
        {
            _repository.Actualizar(siniestro);
            NotificarObservers(siniestro);
        }

        public void Eliminar(int id) => _repository.Eliminar(id);

        private void NotificarObservers(Siniestro siniestro)
        {
            foreach (var observer in _observers)
                observer.Notificar(siniestro);
        }
    }
}