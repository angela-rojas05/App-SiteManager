using SiteManager.Application.Services;
using SiteManager.Domain.Interfaces;
using SiteManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Ruta a la carpeta data
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "data");
Directory.CreateDirectory(dataPath);

// Repositorios
builder.Services.AddScoped<IClienteRepository>(_ => new JsonClienteRepository(dataPath));
builder.Services.AddScoped<ISiniestroRepository>(_ => new JsonSiniestroRepository(dataPath));
builder.Services.AddScoped<IEvidenciaRepository>(_ => new JsonEvidenciaRepository(dataPath));
builder.Services.AddScoped<ICotizacionRepository>(_ => new JsonCotizacionRepository(dataPath));
builder.Services.AddScoped<IMaterialRepository>(_ => new JsonMaterialRepository(dataPath));
builder.Services.AddScoped<IReporteRepository>(_ => new JsonReporteRepository(dataPath));
builder.Services.AddScoped<IUsuarioRepository>(_ => new JsonUsuarioRepository(dataPath));

// Servicios
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<SiniestroService>();
builder.Services.AddScoped<EvidenciaService>();
builder.Services.AddScoped<CotizacionService>();
builder.Services.AddScoped<MaterialService>();
builder.Services.AddScoped<ReporteService>();
builder.Services.AddScoped<UsuarioService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 
