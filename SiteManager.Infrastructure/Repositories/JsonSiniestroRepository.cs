using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonSiniestroRepository : ISiniestroRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonSiniestroRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "siniestros.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Siniestro> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Siniestro>>(json, _options) ?? new List<Siniestro>();
        }

        public Siniestro? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(s => s.Id == id);

        public void Agregar(Siniestro siniestro)
        {
            var lista = ObtenerTodos().ToList();
            siniestro.Id = lista.Count > 0 ? lista.Max(s => s.Id) + 1 : 1;
            siniestro.FechaRegistro = DateTime.Now;
            lista.Add(siniestro);
            Guardar(lista);
        }

        public void Actualizar(Siniestro siniestro)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(s => s.Id == siniestro.Id);
            if (index != -1) { lista[index] = siniestro; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(s => s.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Siniestro> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}