using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class DuplicadosTests
{
    [Fact]
    public void RechazaFutbolistaRepetidoEnPlantilla()
    {
        var detalles = DatosPrueba.Titulares();
        detalles.Add(detalles[0] with { FutbolistaTitular = false });
        var plantilla = new Plantilla(1, "Once", 100_000_000m, 20, 1, detalles);

        Assert.Contains(plantilla.Validar(), error => error.Contains("repetir", StringComparison.OrdinalIgnoreCase));
    }
}
