using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class ComposicionTests
{
    [Fact]
    public void AceptaFormacionDeOnceTitularesRequerida()
    {
        var plantilla = new Plantilla(1, "Once", 100_000_000m, 20, 1, DatosPrueba.Titulares());

        Assert.Empty(plantilla.Validar());
    }

    [Fact]
    public void RechazaFormacionConCantidadIncorrectaPorPuesto()
    {
        var detalles = DatosPrueba.Titulares();
        var mediocampista = detalles[1].Futbolista with { Tipo = TipoFutbolista.Mediocampista };
        detalles[1] = detalles[1] with { Futbolista = mediocampista };
        var plantilla = new Plantilla(1, "Once", 100_000_000m, 20, 1, detalles);

        Assert.Contains(plantilla.Validar(), error => error.Contains("defensores", StringComparison.OrdinalIgnoreCase));
    }
}
