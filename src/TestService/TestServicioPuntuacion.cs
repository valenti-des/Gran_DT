using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;

namespace GranDTApi.Tests.Services;

public class ServiciosPuntuacionTests
{
    private readonly Mock<IRepoPuntuacion> _repoPuntuacionMock = new();
    private readonly ServiciosPuntuacion _serviciosPuntuacion;

    public ServiciosPuntuacionTests()
    {
        _serviciosPuntuacion = new ServiciosPuntuacion(_repoPuntuacionMock.Object);
    }

    [Fact]
    public void Agregar_PuntuacionValida_DevuelvePuntuacionCreada()
    {
        var puntuacion = new Puntuacion { idFutbolista = 1, puntuacion = 10m, numFech = 1 };
        _repoPuntuacionMock.Setup(repo => repo.Agregar(puntuacion)).Returns(puntuacion);

        var resultado = _serviciosPuntuacion.Agregar(puntuacion);

        Assert.Same(puntuacion, resultado);
        _repoPuntuacionMock.Verify(repo => repo.Agregar(puntuacion), Times.Once);
    }

    [Fact]
    public void Agregar_PuntuacionFueraDeRango_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var puntuacion = new Puntuacion { puntuacion = 10.1m, numFech = 1 };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosPuntuacion.Agregar(puntuacion));

        Assert.StartsWith("La puntuación debe estar entre 1.0 y 10.0.", excepcion.Message);
        _repoPuntuacionMock.Verify(repo => repo.Agregar(It.IsAny<Puntuacion>()), Times.Never);
    }

    [Fact]
    public void Agregar_NumeroDeFechaFueraDeRango_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var puntuacion = new Puntuacion { puntuacion = 8m, numFech = 51 };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosPuntuacion.Agregar(puntuacion));

        Assert.StartsWith("El número de fecha debe estar entre 1 y 50.", excepcion.Message);
        _repoPuntuacionMock.Verify(repo => repo.Agregar(It.IsAny<Puntuacion>()), Times.Never);
    }

    [Fact]
    public void ObtenerTodos_ExistenPuntuaciones_DevuelveColeccionCompleta()
    {
        var puntuaciones = new List<Puntuacion>
        {
            new() { idPuntuacion = 1, puntuacion = 8m, numFech = 1 },
            new() { idPuntuacion = 2, puntuacion = 9m, numFech = 2 }
        };
        _repoPuntuacionMock.Setup(repo => repo.ObtenerTodos()).Returns(puntuaciones);

        var resultado = _serviciosPuntuacion.ObtenerTodos();

        Assert.Equal(puntuaciones, resultado);
        _repoPuntuacionMock.Verify(repo => repo.ObtenerTodos(), Times.Once);
    }

    [Fact]
    public void ObtenerPorId_PuntuacionExiste_DevuelvePuntuacion()
    {
        const uint idBuscado = 7;
        var puntuacion = new Puntuacion { idPuntuacion = idBuscado, puntuacion = 9m, numFech = 2 };
        _repoPuntuacionMock.Setup(repo => repo.ObtenerPorId(idBuscado)).Returns(puntuacion);

        var resultado = _serviciosPuntuacion.ObtenerPorId(idBuscado);

        Assert.Same(puntuacion, resultado);
        _repoPuntuacionMock.Verify(repo => repo.ObtenerPorId(idBuscado), Times.Once);
    }

    [Fact]
    public void Eliminar_IdValido_DevuelveResultadoDelRepositorio()
    {
        const uint idAEliminar = 15;
        _repoPuntuacionMock.Setup(repo => repo.Eliminar(idAEliminar)).Returns(true);

        var resultado = _serviciosPuntuacion.Eliminar(idAEliminar);

        Assert.True(resultado);
        _repoPuntuacionMock.Verify(repo => repo.Eliminar(idAEliminar), Times.Once);
    }
}
