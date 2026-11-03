using System;
using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;
using Xunit;

namespace GranDTApi.Tests.Services
{
    public class ServiciosFutbolistaTests
    {
        private readonly Mock<IRepoFutbolista> _repoFutbolistaMock;
        private readonly ServiciosFutbolista _serviciosFutbolista;

        public ServiciosFutbolistaTests()
        {
            // Inicialización de mocks y clase bajo prueba antes de cada test
            _repoFutbolistaMock = new Mock<IRepoFutbolista>();

            _serviciosFutbolista = new ServiciosFutbolista(_repoFutbolistaMock.Object);
        }

        #region Casos de Creación

        [Fact]
        public void Crear_FutbolistaValido_DevuelveIdCreado()
        {
            // Arrange
            var futbolista = new Futbolista
            {
                idFutbolista = 12,
                nombre = "Lionel",
                apellido = "Messi",
                cotizacion = 50_000_000m
            };

            // Simular inserción exitosa en el repositorio
            _repoFutbolistaMock.Setup(repo => repo.Agregar(futbolista))
                               .Returns(futbolista);

            // Act
            var resultado = _serviciosFutbolista.Crear(futbolista);

            // Assert
            Assert.Equal(12, resultado);
            _repoFutbolistaMock.Verify(repo => repo.Agregar(futbolista), Times.Once);
        }

        [Fact]
        public void Crear_NombreOApellidoVacios_LanzaArgumentException()
        {
            // Arrange
            var futbolista = new Futbolista
            {
                nombre = "",
                apellido = "Messi"
            };

            // Act & Assert
            var excepcion = Assert.Throws<ArgumentException>(() => _serviciosFutbolista.Crear(futbolista));

            Assert.StartsWith("El nombre y apellido del futbolista son obligatorios.", excepcion.Message);
            _repoFutbolistaMock.Verify(repo => repo.Agregar(It.IsAny<Futbolista>()), Times.Never);
        }

        #endregion

        #region Casos de Consulta y Filtrado

        [Fact]
        public void ObtenerTodos_ExistenFutbolistas_DevuelveColeccionCompleta()
        {
            // Arrange
            var futbolistasEsperados = new List<Futbolista>
            {
                new Futbolista { idFutbolista = 1, nombre = "Emiliano", apellido = "Martinez" },
                new Futbolista { idFutbolista = 2, nombre = "Cristian", apellido = "Romero" }
            };

            _repoFutbolistaMock.Setup(repo => repo.ObtenerTodos())
                               .Returns(futbolistasEsperados);

            // Act
            var resultado = _serviciosFutbolista.ObtenerTodos();

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            _repoFutbolistaMock.Verify(repo => repo.ObtenerTodos(), Times.Once);
        }

        [Fact]
        public void ObtenerPorId_FutbolistaExiste_DevuelveFutbolista()
        {
            // Arrange
            const int idBuscado = 7;
            var futbolistaEsperado = new Futbolista
            {
                idFutbolista = (ushort)idBuscado,
                nombre = "Rodrigo",
                apellido = "De Paul"
            };

            _repoFutbolistaMock.Setup(repo => repo.ObtenerPorId(idBuscado.ToString()))
                               .Returns(futbolistaEsperado);

            // Act
            var resultado = _serviciosFutbolista.ObtenerPorId(idBuscado);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal("Rodrigo", resultado.nombre);
            Assert.Equal("De Paul", resultado.apellido);
            _repoFutbolistaMock.Verify(repo => repo.ObtenerPorId(idBuscado.ToString()), Times.Once);
        }

        #endregion

        #region Casos de Eliminación

        [Fact]
        public void Eliminar_IdValido_RetornaTrue()
        {
            // Arrange
            const ushort idAEliminar = 15;

            _repoFutbolistaMock.Setup(repo => repo.Eliminar(idAEliminar.ToString()))
                               .Returns(true);

            // Act
            bool resultado = _serviciosFutbolista.Eliminar(idAEliminar);

            // Assert
            Assert.True(resultado);
            _repoFutbolistaMock.Verify(repo => repo.Eliminar(idAEliminar.ToString()), Times.Once);
        }

        #endregion
    }
}