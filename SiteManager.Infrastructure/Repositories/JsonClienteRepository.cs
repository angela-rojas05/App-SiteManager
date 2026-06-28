using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonClienteRepository : IClienteRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonClienteRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "clientes.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Cliente> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Cliente>>(json, _options) ?? new List<Cliente>();
        }

        public Cliente? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(c => c.Id == id);

        public void Agregar(Cliente cliente)
        {
            var lista = ObtenerTodos().ToList();
            cliente.Id = lista.Count > 0 ? lista.Max(c => c.Id) + 1 : 1;
            lista.Add(cliente);
            Guardar(lista);
        }

        public void Actualizar(Cliente cliente)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(c => c.Id == cliente.Id);
            if (index != -1) { lista[index] = cliente; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(c => c.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Cliente> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}