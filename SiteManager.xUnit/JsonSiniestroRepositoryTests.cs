using SiteManager.Domain.Models;
using SiteManager.Infrastructure.Repositories;
using Xunit;

namespace SiteManager.xUnit
{
    public class JsonSiniestroRepositoryTests : IDisposable
    {
        private readonly string _tempPath;
        private readonly JsonSiniestroRepository _repository;

        public JsonSiniestroRepositoryTests()
        {
            _tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(_tempPath);
            _repository = new JsonSiniestroRepository(_tempPath);
        }

        [Fact]
        public void Agregar_Siniestro_SeGuardaEnArchivo()
        {
            // Arrange
            var siniestro = new Siniestro { TipoDanio = "Grieta", Direccion = "Calle 10" };

            // Act
            _repository.Agregar(siniestro);

            // Assert
            var resultado = _repository.ObtenerTodos();
            Assert.Single(resultado);
            Assert.Equal("Grieta", resultado.First().TipoDanio);
        }

        [Fact]
        public void ObtenerPorId_SiniestroExiste_RetornaCorrecto()
        {
            // Arrange
            _repository.Agregar(new Siniestro { TipoDanio = "Humedad", Direccion = "Av. Norte" });

            // Act
            var resultado = _repository.ObtenerPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Humedad", resultado.TipoDanio);
        }

        [Fact]
        public void Eliminar_Siniestro_YaNoExisteEnArchivo()
        {
            // Arrange
            _repository.Agregar(new Siniestro { TipoDanio = "Inundación", Direccion = "Zona Sur" });

            // Act
            _repository.Eliminar(1);

            // Assert
            Assert.Empty(_repository.ObtenerTodos());
        }

        public void Dispose()
        {
            Directory.Delete(_tempPath, true);
        }
    }
}