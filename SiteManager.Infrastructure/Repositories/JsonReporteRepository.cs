using SiteManager.Domain.Interfaces;
using SiteManager.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonReporteRepository : IReporteRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonReporteRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "reportes.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Reporte> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Reporte>>(json, _options) ?? new List<Reporte>();
        }

        public Reporte? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(r => r.Id == id);

        public void Agregar(Reporte reporte)
        {
            var lista = ObtenerTodos().ToList();
            reporte.Id = lista.Count > 0 ? lista.Max(r => r.Id) + 1 : 1;
            reporte.FechaGeneracion = DateTime.Now;
            lista.Add(reporte);
            Guardar(lista);
        }

        public void Actualizar(Reporte reporte)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(r => r.Id == reporte.Id);
            if (index != -1) { lista[index] = reporte; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(r => r.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Reporte> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}