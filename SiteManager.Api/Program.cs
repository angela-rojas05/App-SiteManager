using SiteManager.Application.Services;
using SiteManager.Domain.Interfaces;
using SiteManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ruta a la carpeta data
var dataPath = Path.Combine(builder.Environment.ContentRootPath, "data");
Directory.CreateDirectory(dataPath);

// Repositorios
builder.Services.AddScoped<ISiniestroRepository>(_ => new JsonSiniestroRepository(dataPath));
builder.Services.AddScoped<IClienteRepository>(_ => new JsonClienteRepository(dataPath));

// Servicios
builder.Services.AddScoped<SiniestroService>();
builder.Services.AddScoped<ClienteService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();