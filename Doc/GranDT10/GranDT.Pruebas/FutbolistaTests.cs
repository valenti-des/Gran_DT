using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class FutbolistaTests
{
    [Fact]
    public void AceptaCotizacionEnElLimite()
    {
        Futbolista.ValidarDatos("Nombre", "Apellido", 99_999_999.99m, 1, TipoFutbolista.Arquero);
    }

    [Theory]
    [InlineData(-0.01)]
    [InlineData(100_000_000)]
    public void RechazaCotizacionFueraDeRango(decimal cotizacion)
    {
        Assert.Throws<ReglaNegocioException>(() =>
            Futbolista.ValidarDatos("Nombre", "Apellido", cotizacion, 1, TipoFutbolista.Arquero));
    }
}
