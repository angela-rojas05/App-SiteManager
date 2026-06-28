using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonCotizacionRepository : ICotizacionRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonCotizacionRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "cotizaciones.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Cotizacion> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Cotizacion>>(json, _options) ?? new List<Cotizacion>();
        }

        public Cotizacion? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(c => c.Id == id);

        public void Agregar(Cotizacion cotizacion)
        {
            var lista = ObtenerTodos().ToList();
            cotizacion.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            cotizacion.FechaCotizacion = DateTime.Now;
            lista.Add(cotizacion);
            Guardar(lista);
        }

        public void Actualizar(Cotizacion cotizacion)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(c => c.Id == cotizacion.Id);
            if (index != -1) { lista[index] = cotizacion; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(c => c.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Cotizacion> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}