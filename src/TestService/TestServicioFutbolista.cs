using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;

namespace GranDTApi.Tests.Services;

public class ServiciosFutbolistaTests
{
    private readonly Mock<IRepoFutbolista> _repoFutbolistaMock = new();
    private readonly ServiciosFutbolista _serviciosFutbolista;

    public ServiciosFutbolistaTests()
    {
        _serviciosFutbolista = new ServiciosFutbolista(_repoFutbolistaMock.Object);
    }

    [Fact]
    public void Agregar_FutbolistaValido_DevuelveFutbolistaCreado()
    {
        var futbolista = new Futbolista
        {
            nombre = "Lionel",
            apellido = "Messi",
            fechaNac = new DateOnly(1987, 6, 24),
            cotizacion = 50_000_000m,
            puntuaciones = [new Puntuacion { puntuacion = 10m,
                                             numFech = 1 }]
        };
        _repoFutbolistaMock.Setup(repo => repo.Agregar(futbolista)).Returns(futbolista);

        var resultado = _serviciosFutbolista.Agregar(futbolista);

        Assert.Same(futbolista, resultado);
        _repoFutbolistaMock.Verify(repo => repo.Agregar(futbolista), Times.Once);
    }

    [Fact]
    public void Agregar_FechaInvalida_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var futbolista = new Futbolista
        {
            nombre = "Lionel",
            apellido = "Messi",
            fechaNac = default,
            puntuaciones = [new Puntuacion { puntuacion = 10m, numFech = 1 }]
        };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosFutbolista.Agregar(futbolista));

        Assert.StartsWith("La fecha de nacimiento del futbolista es inválida.", excepcion.Message);
        _repoFutbolistaMock.Verify(repo => repo.Agregar(It.IsAny<Futbolista>()), Times.Never);
    }

    [Fact]
    public void ObtenerTodos_ExistenFutbolistas_DevuelveColeccionCompleta()
    {
        var futbolistas = new List<Futbolista>
        {
            new() { idFutbolista = 1,
                    nombre = "Emiliano",
                    apellido = "Martinez" },
            new() { idFutbolista = 2,
                    nombre = "Cristian",
                    apellido = "Romero" }
        };
        _repoFutbolistaMock.Setup(repo => repo.ObtenerTodos()).Returns(futbolistas);

        var resultado = _serviciosFutbolista.ObtenerTodos();

        Assert.Equal(futbolistas, resultado);
        _repoFutbolistaMock.Verify(repo => repo.ObtenerTodos(), Times.Once);
    }

    [Fact]
    public void ObtenerPorId_FutbolistaExiste_DevuelveFutbolista()
    {
        const ushort idBuscado = 7;
        var futbolista = new Futbolista
        {
            idFutbolista = idBuscado,
            nombre = "Rodrigo",
            apellido = "De Paul"
        };
        _repoFutbolistaMock.Setup(repo => repo.ObtenerPorId(idBuscado)).Returns(futbolista);

        var resultado = _serviciosFutbolista.ObtenerPorId(idBuscado);

        Assert.Same(futbolista, resultado);
        _repoFutbolistaMock.Verify(repo => repo.ObtenerPorId(idBuscado), Times.Once);
    }

    [Fact]
    public void Eliminar_IdValido_DevuelveResultadoDelRepositorio()
    {
        const ushort idAEliminar = 15;
        _repoFutbolistaMock.Setup(repo => repo.Eliminar(idAEliminar)).Returns(true);

        var resultado = _serviciosFutbolista.Eliminar(idAEliminar);

        Assert.True(resultado);
        _repoFutbolistaMock.Verify(repo => repo.Eliminar(idAEliminar), Times.Once);
    }
}
