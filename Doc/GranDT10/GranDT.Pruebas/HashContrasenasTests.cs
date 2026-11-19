using GranDT.Infraestructura;
using Xunit;

namespace GranDT.Pruebas;

public sealed class HashContrasenasTests
{
    [Fact]
    public void ProduceHashNoReversibleConSalAleatoria()
    {
        var hasher = new HashContrasenas();
        const string contrasena = "ClaveTemporal123";
        var primerHash = hasher.Crear(contrasena);
        var segundoHash = hasher.Crear(contrasena);

        Assert.StartsWith("pbkdf2-sha256$", primerHash);
        Assert.DoesNotContain(contrasena, primerHash);
        Assert.NotEqual(primerHash, segundoHash);
    }
}
