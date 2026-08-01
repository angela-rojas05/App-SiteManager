using Microsoft.EntityFrameworkCore;
using SiteManager.Application.Services;
using SiteManager.Domain.Interfaces;
using SiteManager.Infrastructure.Data;
using SiteManager.Infrastructure.Observers;
using SiteManager.Infrastructure.Repositories.Ef;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// PostgreSQL con EF Core
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<SiteManagerContext>(options =>
    options.UseNpgsql(connectionString));

// Repositorios EF
builder.Services.AddScoped<IClienteRepository, EfClienteRepository>();
builder.Services.AddScoped<ISiniestroRepository, EfSiniestroRepository>();
builder.Services.AddScoped<IEvidenciaRepository, EfEvidenciaRepository>();
builder.Services.AddScoped<ICotizacionRepository, EfCotizacionRepository>();
builder.Services.AddScoped<IMaterialRepository, EfMaterialRepository>();
builder.Services.AddScoped<IReporteRepository, EfReporteRepository>();
builder.Services.AddScoped<IUsuarioRepository, EfUsuarioRepository>();

// Observers
builder.Services.AddScoped<ISiniestroObserver, EmailObserver>();

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