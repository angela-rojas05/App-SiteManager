using SiteManager.Application.Services;
using SiteManager.Domain.Models;
using SiteManager.xUnit.Fakes;
using Xunit;

namespace SiteManager.xUnit
{
    public class SiniestroServiceTests
    {
        private SiniestroService CrearServicio(out FakeSiniestroObserver observer)
        {
            observer = new FakeSiniestroObserver();
            return new SiniestroService(
                new FakeSiniestroRepository(),
                new List<FakeSiniestroObserver> { observer });
        }

        [Fact]
        public void Agregar_SiniestroNuevo_SeGuardaCorrectamente()
        {
            // Arrange
            var service = CrearServicio(out _);
            var siniestro = new Siniestro { TipoDanio = "Filtración", Direccion = "Calle 5" };

            // Act
            service.Agregar(siniestro);

            // Assert
            var resultado = service.ObtenerTodos();
            Assert.Single(resultado);
            Assert.Equal("Filtración", resultado.First().TipoDanio);
        }

        [Fact]
        public void ObtenerTodos_ConSiniestros_RetornaLista()
        {
            // Arrange
            var service = CrearServicio(out _);
            service.Agregar(new Siniestro { TipoDanio = "Grieta", Direccion = "Av. Central" });
            service.Agregar(new Siniestro { TipoDanio = "Humedad", Direccion = "Col. Norte" });

            // Act
            var resultado = service.ObtenerTodos();

            // Assert
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void Actualizar_Siniestro_NotificaAlObserver()
        {
            // Arrange
            var service = CrearServicio(out var observer);
            service.Agregar(new Siniestro { TipoDanio = "Inundación", Direccion = "Zona Sur" });
            var siniestroActualizado = new Siniestro { Id = 1, TipoDanio = "Inundación resuelta", Direccion = "Zona Sur" };

            // Act
            service.Actualizar(siniestroActualizado);

            // Assert
            Assert.Single(observer.SiniestrosNotificados);
            Assert.Equal("Inundación resuelta", observer.SiniestrosNotificados.First().TipoDanio);
        }

        [Fact]
        public void Eliminar_SiniestroExistente_YaNoEstaEnLaLista()
        {
            // Arrange
            var service = CrearServicio(out _);
            service.Agregar(new Siniestro { TipoDanio = "Desprendimiento", Direccion = "Blvd. Este" });

            // Act
            service.Eliminar(1);

            // Assert
            Assert.Empty(service.ObtenerTodos());
        }
    }
}