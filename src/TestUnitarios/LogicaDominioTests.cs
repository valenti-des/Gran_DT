using System.ComponentModel.DataAnnotations;
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
                new Puntuacion { numFech = 1, puntuacion = 8.4m },
                new Puntuacion { numFech = 2, puntuacion = 6.5m }
            ]
        };

        Assert.Equal(6.5m, futbolista.ObtenerPuntuacionPorFecha(2));
    }

    [Fact]
    public void ObtenerPuntuacionPorFecha_SinPuntuacionParaLaFecha_DeberiaRetornarCero()
    {
        var futbolista = new Futbolista
        {
            puntuaciones = [new Puntuacion { numFech = 1, puntuacion = 8.4m }]
        };

        Assert.Equal(0m, futbolista.ObtenerPuntuacionPorFecha(2));
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
                        puntuaciones = [new Puntuacion { numFech = 1, puntuacion = 8.4m }]
                    }
                },
                new PlantillaFutbolista
                {
                    futbolistaTitular = false,
                    futbolista = new Futbolista
                    {
                        puntuaciones = [new Puntuacion { numFech = 1, puntuacion = 9m }]
                    }
                },
                new PlantillaFutbolista
                {
                    futbolistaTitular = true,
                    futbolista = null
                }
            ]
        };

        Assert.Equal(8.4m, plantilla.PuntajeFecha(1));
    }

    [Fact]
    public void PuntajeFecha_SinDetalles_DeberiaRetornarCero()
    {
        var plantilla = new Plantilla();

        Assert.Equal(0m, plantilla.PuntajeFecha(1));
    }

    [Fact]
    public void Puntuacion_NumFech_DeberiaSerMenorA50()
    {
        var puntuacion = new Puntuacion { numFech = 50, puntuacion = 7.5m };
        var resultados = new List<ValidationResult>();

        var esValida = Validator.TryValidateObject(
            puntuacion,
            new ValidationContext(puntuacion),
            resultados,
            validateAllProperties: true);

        Assert.False(esValida);
        Assert.Contains(resultados, r => r.MemberNames.Contains(nameof(Puntuacion.numFech)));
    }
}
