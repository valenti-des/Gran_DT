using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraFutbolista
{
    [Fact]
    public void ObtenerPuntuacionPorFecha_DeberiaRetornarLaPuntuacionDeLaJornada()
    {
        var futbolista = new Futbolista
        {
            puntuaciones =
            [
                new Puntuacion { numFech = 1, puntuacion = 8.4m },
                new Puntuacion { numFech = 2, puntuacion = 6.5m }
            ]
        };

        Assert.Equal(6.5m, futbolista.ObtenerPuntuacionPorFecha(2));
    }

    [Fact]
    public void ObtenerPuntuacionPorFecha_SinPuntuacion_DeberiaRetornarCero()
    {
        var futbolista = new Futbolista
        {
            puntuaciones = [new Puntuacion { numFech = 1, puntuacion = 8.4m }]
        };

        Assert.Equal(0m, futbolista.ObtenerPuntuacionPorFecha(2));
    }

    [Fact]
    public void FutbolistaCreacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var futbolista = new FutbolistaCreacion();

        Assert.Equal(string.Empty, futbolista.nombre);
        Assert.Equal(string.Empty, futbolista.apellido);
        Assert.Null(futbolista.apodo);
        Assert.Equal(default, futbolista.fechaNac);
        Assert.Equal(0m, futbolista.cotizacion);
        Assert.Equal((byte)0, futbolista.idEquipo);
        Assert.Equal((byte)0, futbolista.idTipoFutbolista);
        Assert.NotNull(futbolista.puntuaciones);
        Assert.Empty(futbolista.puntuaciones);

        futbolista.nombre = "Franco";
        futbolista.apellido = "Armani";
        futbolista.apodo = "El Pulpo";
        futbolista.fechaNac = new DateOnly(1986, 10, 16);
        futbolista.cotizacion = 15000000m;
        futbolista.idEquipo = 1;
        futbolista.idTipoFutbolista = 1;

        Assert.Equal("Franco", futbolista.nombre);
        Assert.Equal("Armani", futbolista.apellido);
        Assert.Equal("El Pulpo", futbolista.apodo);
        Assert.Equal(new DateOnly(1986, 10, 16), futbolista.fechaNac);
        Assert.Equal(15000000m, futbolista.cotizacion);
        Assert.Equal((byte)1, futbolista.idEquipo);
        Assert.Equal((byte)1, futbolista.idTipoFutbolista);
    }
}
