using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;

namespace GranDTApi.Tests.Services;

public class ServiciosPlantillaTests
{
    private readonly Mock<IRepoPlantilla> _repoPlantillaMock = new();
    private readonly ServiciosPlantilla _serviciosPlantilla;

    public ServiciosPlantillaTests()
    {
        _serviciosPlantilla = new ServiciosPlantilla(_repoPlantillaMock.Object);
    }

    [Fact]
    public void Agregar_PlantillaValida_DevuelvePlantillaCreada()
    {
        var plantilla = new Plantilla
        {
            nombreP = "  Mi plantilla  ",
            presupuesto = 100m,
            cantFutbolista = 20
        };
        _repoPlantillaMock.Setup(repo => repo.Agregar(plantilla)).Returns(plantilla);

        var resultado = _serviciosPlantilla.Agregar(plantilla);

        Assert.Same(plantilla, resultado);
        Assert.Equal("Mi plantilla", plantilla.nombreP);
        _repoPlantillaMock.Verify(repo => repo.Agregar(plantilla), Times.Once);
    }

    [Fact]
    public void Agregar_NombreVacio_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var plantilla = new Plantilla { nombreP = " " };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosPlantilla.Agregar(plantilla));

        Assert.StartsWith("La plantilla debe tener un nombre.", excepcion.Message);
        _repoPlantillaMock.Verify(repo => repo.Agregar(It.IsAny<Plantilla>()), Times.Never);
    }

    [Fact]
    public void Agregar_MontoNegativo_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var plantilla = new Plantilla
        {
            nombreP = "Mi plantilla",
            presupuesto = -1m,
            cantFutbolista = 20
        };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosPlantilla.Agregar(plantilla));

        Assert.StartsWith("El presupuesto no puede ser negativo.", excepcion.Message);
        _repoPlantillaMock.Verify(repo => repo.Agregar(It.IsAny<Plantilla>()), Times.Never);
    }

    [Fact]
    public void Agregar_CantidadMaximaDeFutbolistasFueraDeRango_LanzaArgumentException()
    {
        var plantilla = new Plantilla
        {
            nombreP = "Mi plantilla",
            presupuesto = 100m,
            cantFutbolista = 21
        };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosPlantilla.Agregar(plantilla));

        Assert.StartsWith("La cantidad máxima de futbolistas por plantilla debe ser entre 1 y 20.", excepcion.Message);
        _repoPlantillaMock.Verify(repo => repo.Agregar(It.IsAny<Plantilla>()), Times.Never);
    }

    [Fact]
    public void ObtenerTodos_ExistenPlantillas_DevuelveColeccionCompleta()
    {
        var plantillas = new List<Plantilla>
        {
            new() { idPlantilla = 1, nombreP = "Titulares" },
            new() { idPlantilla = 2, nombreP = "Suplentes" }
        };
        _repoPlantillaMock.Setup(repo => repo.ObtenerTodos()).Returns(plantillas);

        var resultado = _serviciosPlantilla.ObtenerTodos();

        Assert.Equal(plantillas, resultado);
        _repoPlantillaMock.Verify(repo => repo.ObtenerTodos(), Times.Once);
    }

    [Fact]
    public void ObtenerPorId_PlantillaExiste_DevuelvePlantilla()
    {
        const ushort idBuscado = 7;
        var plantilla = new Plantilla { idPlantilla = (byte)idBuscado, nombreP = "Titulares" };
        _repoPlantillaMock.Setup(repo => repo.ObtenerPorId(idBuscado)).Returns(plantilla);

        var resultado = _serviciosPlantilla.ObtenerPorId(idBuscado);

        Assert.Same(plantilla, resultado);
        _repoPlantillaMock.Verify(repo => repo.ObtenerPorId(idBuscado), Times.Once);
    }

    [Fact]
    public void Eliminar_IdValido_DevuelveResultadoDelRepositorio()
    {
        const ushort idAEliminar = 15;
        _repoPlantillaMock
            .Setup(repo => repo.ObtenerPorId(idAEliminar))
            .Returns(new Plantilla { idPlantilla = idAEliminar, idUsuario = 7 });
        _repoPlantillaMock.Setup(repo => repo.Eliminar(idAEliminar)).Returns(true);

        var resultado = _serviciosPlantilla.Eliminar(idAEliminar, 7);

        Assert.True(resultado);
        _repoPlantillaMock.Verify(repo => repo.Eliminar(idAEliminar), Times.Once);
    }

    [Fact]
    public void Eliminar_PlantillaDeOtroUsuario_NoLlamaAlRepositorio()
    {
        const uint idPlantilla = 15;
        _repoPlantillaMock
            .Setup(repo => repo.ObtenerPorId(idPlantilla))
            .Returns(new Plantilla { idPlantilla = idPlantilla, idUsuario = 8 });

        var resultado = _serviciosPlantilla.Eliminar(idPlantilla, 7);

        Assert.False(resultado);
        _repoPlantillaMock.Verify(repo => repo.Eliminar(It.IsAny<uint>()), Times.Never);
    }

}
