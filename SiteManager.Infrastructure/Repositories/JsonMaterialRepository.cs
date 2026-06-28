using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonMaterialRepository : IMaterialRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonMaterialRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "materiales.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Material> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Material>>(json, _options) ?? new List<Material>();
        }

        public Material? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(m => m.Id == id);

        public void Agregar(Material material)
        {
            var lista = ObtenerTodos().ToList();
            material.Id = lista.Count > 0 ? lista.Max(m => m.Id) + 1 : 1;
            lista.Add(material);
            Guardar(lista);
        }

        public void Actualizar(Material material)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(m => m.Id == material.Id);
            if (index != -1) { lista[index] = material; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(m => m.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Material> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}