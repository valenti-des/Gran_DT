using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraPlantillaFutbolista
{
    [Fact]
    public void PlantillaFutbolistaCreacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var detalle = new PlantillaFutbolistaCreacion();

        Assert.False(detalle.futbolistaTitular);
        Assert.Equal((byte)0, detalle.idPlantilla);
        Assert.Equal((ushort)0, detalle.idFutbolista);
        Assert.Null(detalle.futbolista);

        var futbolista = new Futbolista { nombre = "Jugador" };
        detalle.futbolistaTitular = true;
        detalle.idPlantilla = 2;
        detalle.idFutbolista = 3;
        detalle.futbolista = futbolista;

        Assert.True(detalle.futbolistaTitular);
        Assert.Equal((byte)2, detalle.idPlantilla);
        Assert.Equal((ushort)3, detalle.idFutbolista);
        Assert.Same(futbolista, detalle.futbolista);
    }
}
