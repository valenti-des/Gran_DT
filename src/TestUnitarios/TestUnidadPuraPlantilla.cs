using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraPlantilla
{
    [Fact]
    public void PuntajeFecha_DeberiaSumarSoloLasPuntuacionesDeTitulares()
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
                new PlantillaFutbolista { futbolistaTitular = true }
            ]
        };

        Assert.Equal(8.4m, plantilla.PuntajeFecha(1));
    }

    [Fact]
    public void PuntajeFecha_DeberiaSumarVariosTitularesDeLaJornadaSolicitada()
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
                        puntuaciones =
                        [
                            new Puntuacion { numFech = 1, puntuacion = 8.4m },
                            new Puntuacion { numFech = 2, puntuacion = 7m }
                        ]
                    }
                },
                new PlantillaFutbolista
                {
                    futbolistaTitular = true,
                    futbolista = new Futbolista
                    {
                        puntuaciones = [new Puntuacion { numFech = 1, puntuacion = 6.5m }]
                    }
                }
            ]
        };

        Assert.Equal(14.9m, plantilla.PuntajeFecha(1));
    }

    [Fact]
    public void PuntajeFecha_SinDetalles_DeberiaRetornarCero()
    {
        Assert.Equal(0m, new Plantilla().PuntajeFecha(1));
    }

    [Fact]
    public void PlantillaCreacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var plantilla = new PlantillaCreacion();

        Assert.Equal(string.Empty, plantilla.nombreP);
        Assert.Null(plantilla.idUsuario);
        Assert.Empty(plantilla.detalles);

        plantilla.nombreP = "Plantilla de prueba";
        plantilla.idUsuario = 2;
        plantilla.detalles.Add(new PlantillaFutbolista { idFutbolista = 3 });

        Assert.Equal("Plantilla de prueba", plantilla.nombreP);
        Assert.Equal((short)2, plantilla.idUsuario);
        Assert.Single(plantilla.detalles);
    }
}
