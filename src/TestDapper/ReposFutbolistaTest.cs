using BiblioConDapper;

namespace Gran_DT.TestDapper;

public class RepoTipoFutbolistaTest : RepoTest
{
    [Fact]
    public void ObtenerTiposFutbolistas_DevuelveFilasValidas()
    {
        var repo = new RepoTipoFutbolista(Conexion);
        var tipos = repo.obtenerTiposFutbolistas();

        Assert.All(tipos, tipo =>
        {
            Assert.NotEqual((byte)0, tipo.idTipoFutbolista);
            Assert.False(string.IsNullOrWhiteSpace(tipo.tipoFutbolista));
        });
    }
}

public sealed class MySqlFactAttribute : FactAttribute
{
    public MySqlFactAttribute()
    {
        if (string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("GRAN_DT_TEST_CONNECTION_STRING")))
        {
            Skip = "Configura GRAN_DT_TEST_CONNECTION_STRING para ejecutar pruebas de integración con MySQL.";
        }
    }
}