using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;

namespace SiteManager.xUnit.Fakes
{
    public class FakeSiniestroRepository : ISiniestroRepository
    {
        private readonly List<Siniestro> _siniestros = new();

        public IEnumerable<Siniestro> ObtenerTodos() => _siniestros;

        public Siniestro? ObtenerPorId(int id) => _siniestros.FirstOrDefault(s => s.Id == id);

        public void Agregar(Siniestro siniestro)
        {
            siniestro.Id = _siniestros.Count > 0 ? _siniestros.Max(s => s.Id) + 1 : 1;
            _siniestros.Add(siniestro);
        }

        public void Actualizar(Siniestro siniestro)
        {
            var index = _siniestros.FindIndex(s => s.Id == siniestro.Id);
            if (index != -1) _siniestros[index] = siniestro;
        }

        public void Eliminar(int id) => _siniestros.RemoveAll(s => s.Id == id);
    }
}