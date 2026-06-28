using SiteManager.Domain.Interfaces;
using SiteManager.Domain.Models;
using System.Text.Json;

namespace SiteManager.Infrastructure.Repositories
{
    public class JsonUsuarioRepository : IUsuarioRepository
    {
        private readonly string _path;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public JsonUsuarioRepository(string dataPath)
        {
            _path = Path.Combine(dataPath, "usuarios.json");
            if (!File.Exists(_path))
                File.WriteAllText(_path, "[]");
        }

        public IEnumerable<Usuario> ObtenerTodos()
        {
            var json = File.ReadAllText(_path);
            return JsonSerializer.Deserialize<List<Usuario>>(json, _options) ?? new List<Usuario>();
        }

        public Usuario? ObtenerPorId(int id) => ObtenerTodos().FirstOrDefault(u => u.Id == id);

        public void Agregar(Usuario usuario)
        {
            var lista = ObtenerTodos().ToList();
            usuario.Id = lista.Count > 0 ? lista.Max(u => u.Id) + 1 : 1;
            usuario.FechaRegistro = DateTime.Now;
            lista.Add(usuario);
            Guardar(lista);
        }

        public void Actualizar(Usuario usuario)
        {
            var lista = ObtenerTodos().ToList();
            var index = lista.FindIndex(u => u.Id == usuario.Id);
            if (index != -1) { lista[index] = usuario; Guardar(lista); }
        }

        public void Eliminar(int id)
        {
            var lista = ObtenerTodos().ToList();
            lista.RemoveAll(u => u.Id == id);
            Guardar(lista);
        }

        private void Guardar(List<Usuario> lista) =>
            File.WriteAllText(_path, JsonSerializer.Serialize(lista, _options));
    }
}