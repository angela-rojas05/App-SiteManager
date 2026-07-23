using SiteManager.Application.Services;
using SiteManager.Domain.Models;
using SiteManager.xUnit.Fakes;
using Xunit;

namespace SiteManager.xUnit
{
    public class ClienteServiceTests
    {
        private ClienteService CrearServicio()
        {
            return new ClienteService(new FakeClienteRepository());
        }

        [Fact]
        public void Agregar_ClienteNuevo_SeGuardaCorrectamente()
        {
            // Arrange
            var service = CrearServicio();
            var cliente = new Cliente { Nombre = "Ana López", Correo = "ana@correo.com" };

            // Act
            service.Agregar(cliente);

            // Assert
            var resultado = service.ObtenerTodos();
            Assert.Single(resultado);
            Assert.Equal("Ana López", resultado.First().Nombre);
        }

        [Fact]
        public void ObtenerPorId_ClienteExiste_RetornaCliente()
        {
            // Arrange
            var service = CrearServicio();
            service.Agregar(new Cliente { Nombre = "Carlos Ruiz", Correo = "carlos@correo.com" });

            // Act
            var resultado = service.ObtenerPorId(1);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Carlos Ruiz", resultado.Nombre);
        }

        [Fact]
        public void ObtenerPorId_ClienteNoExiste_RetornaNull()
        {
            // Arrange
            var service = CrearServicio();

            // Act
            var resultado = service.ObtenerPorId(999);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public void Eliminar_ClienteExistente_YaNoEstaEnLaLista()
        {
            // Arrange
            var service = CrearServicio();
            service.Agregar(new Cliente { Nombre = "María García", Correo = "maria@correo.com" });

            // Act
            service.Eliminar(1);

            // Assert
            Assert.Empty(service.ObtenerTodos());
        }

        [Fact]
        public void Actualizar_ClienteExistente_CambiaElNombre()
        {
            // Arrange
            var service = CrearServicio();
            service.Agregar(new Cliente { Nombre = "Pedro Solis", Correo = "pedro@correo.com" });
            var clienteActualizado = new Cliente { Id = 1, Nombre = "Pedro Actualizado", Correo = "pedro@correo.com" };

            // Act
            service.Actualizar(clienteActualizado);

            // Assert
            var resultado = service.ObtenerPorId(1);
            Assert.Equal("Pedro Actualizado", resultado?.Nombre);
        }
    }
}