using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;

namespace GranDTApi.Tests.Services;

public class ServiciosEquipoTests
{
    private readonly Mock<IRepoEquipo> _repoEquipoMock = new();
    private readonly ServiciosEquipo _serviciosEquipo;

    public ServiciosEquipoTests()
    {
        _serviciosEquipo = new ServiciosEquipo(_repoEquipoMock.Object);
    }

    [Fact]
    public void Agregar_EquipoValido_DevuelveEquipoCreado()
    {
        var equipo = new Equipo { nombre = "  River Plate  " };
        _repoEquipoMock.Setup(repo => repo.ObtenerTodos()).Returns([]);
        _repoEquipoMock.Setup(repo => repo.Agregar(equipo)).Returns(equipo);

        var resultado = _serviciosEquipo.Agregar(equipo);

        Assert.Same(equipo, resultado);
        Assert.Equal("River Plate", equipo.nombre);
        _repoEquipoMock.Verify(repo => repo.Agregar(equipo), Times.Once);
    }

    [Fact]
    public void Agregar_NombreVacio_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var equipo = new Equipo { nombre = "  " };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosEquipo.Agregar(equipo));

        Assert.StartsWith("El nombre del equipo es obligatorio.", excepcion.Message);
        _repoEquipoMock.Verify(repo => repo.Agregar(It.IsAny<Equipo>()), Times.Never);
    }

    [Fact]
    public void Agregar_EquipoDuplicado_LanzaArgumentExceptionSinAgregar()
    {
        var equipoExistente = new Equipo { nombre = "River Plate" };
        var equipo = new Equipo { nombre = " river plate " };
        _repoEquipoMock.Setup(repo => repo.ObtenerTodos()).Returns([equipoExistente]);

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosEquipo.Agregar(equipo));

        Assert.StartsWith("Ya existe un equipo con ese nombre.", excepcion.Message);
        _repoEquipoMock.Verify(repo => repo.Agregar(It.IsAny<Equipo>()), Times.Never);
    }

    [Fact]
    public void ObtenerTodos_ExistenEquipos_DevuelveColeccionCompleta()
    {
        var equipos = new List<Equipo>
        {
            new() { idEquipo = 1, nombre = "River Plate" },
            new() { idEquipo = 2, nombre = "Boca Juniors" }
        };
        _repoEquipoMock.Setup(repo => repo.ObtenerTodos()).Returns(equipos);

        var resultado = _serviciosEquipo.ObtenerTodos();

        Assert.Equal(equipos, resultado);
        _repoEquipoMock.Verify(repo => repo.ObtenerTodos(), Times.Once);
    }

    [Fact]
    public void ObtenerPorId_EquipoExiste_DevuelveEquipo()
    {
        const byte idBuscado = 7;
        var equipo = new Equipo { idEquipo = idBuscado, nombre = "River Plate" };
        _repoEquipoMock.Setup(repo => repo.ObtenerPorId(idBuscado)).Returns(equipo);

        var resultado = _serviciosEquipo.ObtenerPorId(idBuscado);

        Assert.Same(equipo, resultado);
        _repoEquipoMock.Verify(repo => repo.ObtenerPorId(idBuscado), Times.Once);
    }

    [Fact]
    public void Eliminar_IdValido_DevuelveResultadoDelRepositorio()
    {
        const byte idAEliminar = 15;
        _repoEquipoMock.Setup(repo => repo.Eliminar(idAEliminar)).Returns(true);

        var resultado = _serviciosEquipo.Eliminar(idAEliminar);

        Assert.True(resultado);
        _repoEquipoMock.Verify(repo => repo.Eliminar(idAEliminar), Times.Once);
    }
}
