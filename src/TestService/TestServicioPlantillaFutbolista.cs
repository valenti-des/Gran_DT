using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;

namespace GranDTApi.Tests.Services;

public class ServiciosPlantillaFutbolistaTests
{
    private readonly Mock<IRepoPlantillaFutbolista> _repoAsociacionMock = new();
    private readonly Mock<IRepoPlantilla> _repoPlantillaMock = new();
    private readonly Mock<IRepoFutbolista> _repoFutbolistaMock = new();
    private readonly ServiciosPlantillaFutbolista _servicios;

    public ServiciosPlantillaFutbolistaTests()
    {
        _servicios = new ServiciosPlantillaFutbolista(
            _repoAsociacionMock.Object,
            _repoPlantillaMock.Object,
            _repoFutbolistaMock.Object);
    }

    [Fact]
    public void Agregar_AsociacionValida_GuardaElTitular()
    {
        var plantilla = new Plantilla { idPlantilla = 1 };
        var futbolista = new Futbolista
        {
            idFutbolista = 10,
            nombre = "Lionel",
            apellido = "Messi"
        };
        _repoPlantillaMock.Setup(repo => repo.ObtenerPorId(1)).Returns(plantilla);
        _repoFutbolistaMock.Setup(repo => repo.ObtenerPorId(10)).Returns(futbolista);
        _repoAsociacionMock.Setup(repo => repo.Agregar(It.IsAny<PlantillaFutbolista>()))
            .Returns((PlantillaFutbolista asociacion) => asociacion);

        var resultado = _servicios.Agregar(1, 10, true);

        Assert.Equal((uint)1, resultado.idPlantilla);
        Assert.Equal((ushort)10, resultado.idFutbolista);
        Assert.True(resultado.futbolistaTitular);
        Assert.Equal("Lionel Messi", resultado.nombreFutbolista);
        _repoAsociacionMock.Verify(repo => repo.Agregar(It.IsAny<PlantillaFutbolista>()), Times.Once);
    }

    [Fact]
    public void Agregar_PlantillaInexistente_LanzaKeyNotFoundException()
    {
        _repoPlantillaMock.Setup(repo => repo.ObtenerPorId(1)).Returns((Plantilla?)null);

        var excepcion = Assert.Throws<KeyNotFoundException>(() => _servicios.Agregar(1, 10, true));

        Assert.Equal("La plantilla no existe.", excepcion.Message);
        _repoAsociacionMock.Verify(repo => repo.Agregar(It.IsAny<PlantillaFutbolista>()), Times.Never);
    }

    [Fact]
    public void Agregar_FutbolistaInexistente_LanzaKeyNotFoundException()
    {
        _repoPlantillaMock.Setup(repo => repo.ObtenerPorId(1)).Returns(new Plantilla());
        _repoFutbolistaMock.Setup(repo => repo.ObtenerPorId(10)).Returns((Futbolista?)null);

        var excepcion = Assert.Throws<KeyNotFoundException>(() => _servicios.Agregar(1, 10, true));

        Assert.Equal("El futbolista no existe.", excepcion.Message);
        _repoAsociacionMock.Verify(repo => repo.Agregar(It.IsAny<PlantillaFutbolista>()), Times.Never);
    }

    [Fact]
    public void Agregar_FutbolistaYaAsociado_LanzaInvalidOperationException()
    {
        _repoPlantillaMock.Setup(repo => repo.ObtenerPorId(1)).Returns(new Plantilla());
        _repoFutbolistaMock.Setup(repo => repo.ObtenerPorId(10)).Returns(new Futbolista());
        _repoAsociacionMock.Setup(repo => repo.ObtenerPorId(1, 10)).Returns(new PlantillaFutbolista());

        var excepcion = Assert.Throws<InvalidOperationException>(() => _servicios.Agregar(1, 10, true));

        Assert.Equal("El futbolista ya está asociado a esta plantilla.", excepcion.Message);
        _repoAsociacionMock.Verify(repo => repo.Agregar(It.IsAny<PlantillaFutbolista>()), Times.Never);
    }

    [Fact]
    public void Agregar_PlantillaCompleta_LanzaInvalidOperationException()
    {
        var plantillaCompleta = new Plantilla
        {
            detalles = Enumerable.Range(1, 20).Select(_ => new PlantillaFutbolista()).ToList()
        };
        _repoPlantillaMock.Setup(repo => repo.ObtenerPorId(1)).Returns(plantillaCompleta);
        _repoFutbolistaMock.Setup(repo => repo.ObtenerPorId(10)).Returns(new Futbolista());

        var excepcion = Assert.Throws<InvalidOperationException>(() => _servicios.Agregar(1, 10, true));

        Assert.Equal("La plantilla alcanzó su cantidad máxima de futbolistas.", excepcion.Message);
        _repoAsociacionMock.Verify(repo => repo.Agregar(It.IsAny<PlantillaFutbolista>()), Times.Never);
    }
}
