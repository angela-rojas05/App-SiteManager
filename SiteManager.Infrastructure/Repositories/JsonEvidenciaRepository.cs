using SiteManager.Domain.Interfaces;
using SiteManager.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonEvidenciaRepository : IEvidenciaRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonEvidenciaRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "evidencias.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Evidencia> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Evidencia>>(json, _options) ?? new List<Evidencia>();
        }

        public Evidencia? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(e => e.Id == id);

        public void Agregar(Evidencia evidencia)
        {
            var lista = ObtenerTodos().ToList();
            evidencia.Id = lista.Count > 0 ? lista.Max(e => e.Id) + 1 : 1;
            evidencia.FechaSubida = DateTime.Now;
            lista.Add(evidencia);
            Guardar(lista);
        }

        public void Actualizar(Evidencia evidencia)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(e => e.Id == evidencia.Id);
            if (index != -1) { lista[index] = evidencia; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(e => e.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Evidencia> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}