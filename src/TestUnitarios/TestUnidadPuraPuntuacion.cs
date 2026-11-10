using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraPuntuacion
{
    [Fact]
    public void Puntuacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var puntuacion = new Puntuacion();

        Assert.Equal(0u, puntuacion.idPuntuacion);
        Assert.Equal((ushort)0, puntuacion.idFutbolista);
        Assert.Null(puntuacion.puntuacion);
        Assert.Null(puntuacion.numFech);

        puntuacion.idPuntuacion = 1;
        puntuacion.idFutbolista = 2;
        puntuacion.puntuacion = 8.4m;
        puntuacion.numFech = 3;

        Assert.Equal(1u, puntuacion.idPuntuacion);
        Assert.Equal((ushort)2, puntuacion.idFutbolista);
        Assert.Equal(8.4m, puntuacion.puntuacion);
        Assert.Equal((byte)3, puntuacion.numFech);
    }

    [Fact]
    public void PuntuacionCreacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var puntuacion = new PuntuacionCreacion();

        Assert.Equal((ushort)0, puntuacion.idFutbolista);
        Assert.Null(puntuacion.puntuacion);
        Assert.Null(puntuacion.numFech);

        puntuacion.idFutbolista = 2;
        puntuacion.puntuacion = 8.4m;
        puntuacion.numFech = 3;

        Assert.Equal((ushort)2, puntuacion.idFutbolista);
        Assert.Equal(8.4m, puntuacion.puntuacion);
        Assert.Equal((byte)3, puntuacion.numFech);
    }
}
