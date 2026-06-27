using System.Text.Json;
using SiteManager.Models;

namespace SiteManager.Data
{
    public class SiteManagerContext
    {
        private readonly string _dataPath;
        private readonly JsonSerializerOptions _jsonOptions;

        public List<Cliente> Clientes { get; private set; } = new();
        public List<Siniestro> Siniestros { get; private set; } = new();
        public List<Evidencia> Evidencias { get; private set; } = new();
        public List<Cotizacion> Cotizaciones { get; private set; } = new();
        public List<Usuario> Usuarios { get; private set; } = new();
        public List<Material> Materiales { get; private set; } = new();
        public List<Reporte> Reportes { get; private set; } = new();

        public SiteManagerContext()
        {
            _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "AppData");
            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            Directory.CreateDirectory(_dataPath);
            CargarDatos();
        }

        private void CargarDatos()
        {
            Clientes = CargarArchivo<Cliente>("clientes.json");
            Siniestros = CargarArchivo<Siniestro>("siniestros.json");
            Evidencias = CargarArchivo<Evidencia>("evidencias.json");
            Cotizaciones = CargarArchivo<Cotizacion>("cotizaciones.json");
            Usuarios = CargarArchivo<Usuario>("usuarios.json");
            Materiales = CargarArchivo<Material>("materiales.json");
            Reportes = CargarArchivo<Reporte>("reportes.json");
        }

        private List<T> CargarArchivo<T>(string nombreArchivo)
        {
            string ruta = Path.Combine(_dataPath, nombreArchivo);
            if (!File.Exists(ruta))
                return new List<T>();

            string json = File.ReadAllText(ruta);
            return JsonSerializer.Deserialize<List<T>>(json, _jsonOptions) ?? new List<T>();
        }

        public void SaveChanges()
        {
            GuardarArchivo("clientes.json", Clientes);
            GuardarArchivo("siniestros.json", Siniestros);
            GuardarArchivo("evidencias.json", Evidencias);
            GuardarArchivo("cotizaciones.json", Cotizaciones);
            GuardarArchivo("usuarios.json", Usuarios);
            GuardarArchivo("materiales.json", Materiales);
            GuardarArchivo("reportes.json", Reportes);
        }

        private void GuardarArchivo<T>(string nombreArchivo, List<T> datos)
        {
            string ruta = Path.Combine(_dataPath, nombreArchivo);
            string json = JsonSerializer.Serialize(datos, _jsonOptions);
            File.WriteAllText(ruta, json);
        }

        public void Add(Cliente cliente)
        {
            cliente.Id = Clientes.Count > 0 ? Clientes.Max(c => c.Id) + 1 : 1;
            Clientes.Add(cliente);
            SaveChanges();
        }
        public void Update(Cliente cliente)
        {
            var index = Clientes.FindIndex(c => c.Id == cliente.Id);
            if (index >= 0) { Clientes[index] = cliente; SaveChanges(); }
        }
        public void Remove(Cliente cliente)
        {
            Clientes.RemoveAll(c => c.Id == cliente.Id);
            SaveChanges();
        }
        public Cliente? FindCliente(int id) => Clientes.FirstOrDefault(c => c.Id == id);
        public bool ClienteExists(int id) => Clientes.Any(c => c.Id == id);

        public void Add(Siniestro siniestro)
        {
            siniestro.Id = Siniestros.Count > 0 ? Siniestros.Max(s => s.Id) + 1 : 1;
            Siniestros.Add(siniestro);
            SaveChanges();
        }
        public void Update(Siniestro siniestro)
        {
            var index = Siniestros.FindIndex(s => s.Id == siniestro.Id);
            if (index >= 0) { Siniestros[index] = siniestro; SaveChanges(); }
        }
        public void Remove(Siniestro siniestro)
        {
            Siniestros.RemoveAll(s => s.Id == siniestro.Id);
            SaveChanges();
        }
        public Siniestro? FindSiniestro(int id) => Siniestros.FirstOrDefault(s => s.Id == id);
        public bool SiniestroExists(int id) => Siniestros.Any(s => s.Id == id);

        public void Add(Evidencia evidencia)
        {
            evidencia.Id = Evidencias.Count > 0 ? Evidencias.Max(e => e.Id) + 1 : 1;
            Evidencias.Add(evidencia);
            SaveChanges();
        }
        public void Update(Evidencia evidencia)
        {
            var index = Evidencias.FindIndex(e => e.Id == evidencia.Id);
            if (index >= 0) { Evidencias[index] = evidencia; SaveChanges(); }
        }
        public void Remove(Evidencia evidencia)
        {
            Evidencias.RemoveAll(e => e.Id == evidencia.Id);
            SaveChanges();
        }
        public Evidencia? FindEvidencia(int id) => Evidencias.FirstOrDefault(e => e.Id == id);
        public bool EvidenciaExists(int id) => Evidencias.Any(e => e.Id == id);

        public void Add(Cotizacion cotizacion)
        {
            cotizacion.Id = Cotizaciones.Count > 0 ? Cotizaciones.Max(c => c.Id) + 1 : 1;
            Cotizaciones.Add(cotizacion);
            SaveChanges();
        }
        public void Update(Cotizacion cotizacion)
        {
            var index = Cotizaciones.FindIndex(c => c.Id == cotizacion.Id);
            if (index >= 0) { Cotizaciones[index] = cotizacion; SaveChanges(); }
        }
        public void Remove(Cotizacion cotizacion)
        {
            Cotizaciones.RemoveAll(c => c.Id == cotizacion.Id);
            SaveChanges();
        }
        public Cotizacion? FindCotizacion(int id) => Cotizaciones.FirstOrDefault(c => c.Id == id);
        public bool CotizacionExists(int id) => Cotizaciones.Any(c => c.Id == id);

        public void Add(Usuario usuario)
        {
            usuario.Id = Usuarios.Count > 0 ? Usuarios.Max(u => u.Id) + 1 : 1;
            Usuarios.Add(usuario);
            SaveChanges();
        }
        public void Update(Usuario usuario)
        {
            var index = Usuarios.FindIndex(u => u.Id == usuario.Id);
            if (index >= 0) { Usuarios[index] = usuario; SaveChanges(); }
        }
        public void Remove(Usuario usuario)
        {
            Usuarios.RemoveAll(u => u.Id == usuario.Id);
            SaveChanges();
        }
        public Usuario? FindUsuario(int id) => Usuarios.FirstOrDefault(u => u.Id == id);
        public bool UsuarioExists(int id) => Usuarios.Any(u => u.Id == id);

        public void Add(Material material)
        {
            material.Id = Materiales.Count > 0 ? Materiales.Max(m => m.Id) + 1 : 1;
            Materiales.Add(material);
            SaveChanges();
        }
        public void Update(Material material)
        {
            var index = Materiales.FindIndex(m => m.Id == material.Id);
            if (index >= 0) { Materiales[index] = material; SaveChanges(); }
        }
        public void Remove(Material material)
        {
            Materiales.RemoveAll(m => m.Id == material.Id);
            SaveChanges();
        }
        public Material? FindMaterial(int id) => Materiales.FirstOrDefault(m => m.Id == id);
        public bool MaterialExists(int id) => Materiales.Any(m => m.Id == id);

        public void Add(Reporte reporte)
        {
            reporte.Id = Reportes.Count > 0 ? Reportes.Max(r => r.Id) + 1 : 1;
            Reportes.Add(reporte);
            SaveChanges();
        }
        public void Update(Reporte reporte)
        {
            var index = Reportes.FindIndex(r => r.Id == reporte.Id);
            if (index >= 0) { Reportes[index] = reporte; SaveChanges(); }
        }
        public void Remove(Reporte reporte)
        {
            Reportes.RemoveAll(r => r.Id == reporte.Id);
            SaveChanges();
        }
        public Reporte? FindReporte(int id) => Reportes.FirstOrDefault(r => r.Id == id);
        public bool ReporteExists(int id) => Reportes.Any(r => r.Id == id);
    }
}