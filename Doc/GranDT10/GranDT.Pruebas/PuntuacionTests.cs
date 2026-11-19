using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class PuntuacionTests
{
    [Fact]
    public void AceptaPuntuacionEnRango()
    {
        Puntuacion.Validar(8.5m, 7);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(10.1, 1)]
    [InlineData(5, 0)]
    [InlineData(5, 50)]
    public void RechazaPuntuacionOFecchaFueraDeRango(decimal valor, int fecha)
    {
        Assert.Throws<ReglaNegocioException>(() => Puntuacion.Validar(valor, fecha));
    }

    [Fact]
    public void JornadaSinPuntuacionAportaCero()
    {
        var futbolista = new Futbolista(1, "Nombre", "Apellido", null, null, 1, 1, TipoFutbolista.Arquero, []);

        Assert.Equal(0m, futbolista.PuntajeEnFecha(1));
    }
}
