using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class PresupuestoTests
{
    [Fact]
    public void AceptaPlantillaDentroDelPresupuestoMaximo()
    {
        var plantilla = new Plantilla(1, "Once", 100_000_000m, 20, 1, DatosPrueba.Titulares());

        Assert.True(plantilla.PresupuestoValido);
        Assert.DoesNotContain(plantilla.Validar(), error => error.Contains("presupuesto", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void RechazaPlantillaQueSuperaElPresupuesto()
    {
        var plantilla = new Plantilla(1, "Once", 100_000_000m, 20, 1, DatosPrueba.Titulares(10_000_000m));

        Assert.False(plantilla.PresupuestoValido);
        Assert.Contains(plantilla.Validar(), error => error.Contains("presupuesto", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void RechazaPresupuestoConfiguradoSobreElMaximo()
    {
        Assert.Throws<ReglaNegocioException>(() => new Plantilla(1, "Once", 100_000_000.01m, 20, 1));
    }
}
