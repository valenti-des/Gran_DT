using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class LogicaDominioTests
{
    [Fact]
    public void ObtenerPuntuacionPorFecha_DeberiaRetornarLaPuntuacionDeLaFechaSolicitada()
    {
        var futbolista = new Futbolista
        {
            puntuaciones =
            [
                new Puntuacion { cantFech = new DateOnly(2026, 1, 1), puntuacion = 8.4f },
                new Puntuacion { cantFech = new DateOnly(2026, 1, 2), puntuacion = 6.5f }
            ]
        };

        Assert.Equal(6.5f, futbolista.ObtenerPuntuacionPorFecha(new DateOnly(2026, 1, 2)));
    }

    [Fact]
    public void ObtenerPuntuacionPorFecha_SinPuntuacionParaLaFecha_DeberiaRetornarCero()
    {
        var futbolista = new Futbolista
        {
            puntuaciones = [new Puntuacion { cantFech = new DateOnly(2026, 1, 1), puntuacion = 8.4f }]
        };

        Assert.Equal(0f, futbolista.ObtenerPuntuacionPorFecha(new DateOnly(2026, 1, 2)));
    }

    [Fact]
    public void PuntajeFecha_DeberiaSumarSoloLosFutbolistasTitulares()
    {
        var plantilla = new Plantilla
        {
            detalles =
            [
                new PlantillaFutbolista
                {
                    futbolistaTitular = true,
                    futbolista = new Futbolista
                    {
                        puntuaciones = [new Puntuacion { cantFech = new DateOnly(2026, 1, 1), puntuacion = 8.4f }]
                    }
                },
                new PlantillaFutbolista
                {
                    futbolistaTitular = false,
                    futbolista = new Futbolista
                    {
                        puntuaciones = [new Puntuacion { cantFech = new DateOnly(2026, 1, 1), puntuacion = 9f }]
                    }
                },
                new PlantillaFutbolista
                {
                    futbolistaTitular = true,
                    futbolista = null
                }
            ]
        };

        Assert.Equal(8.4f, plantilla.PuntajeFecha(new DateOnly(2026, 1, 1)));
    }

    [Fact]
    public void PuntajeFecha_SinDetalles_DeberiaRetornarCero()
    {
        var plantilla = new Plantilla();

        Assert.Equal(0f, plantilla.PuntajeFecha(new DateOnly(2026, 1, 1)));
    }
}
