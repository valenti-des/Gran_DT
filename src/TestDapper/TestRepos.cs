using System.Data;
using MySqlConnector;

namespace Gran_DT.TestDapper;

public class RepoTest
{
    protected readonly IDbConnection Conexion;

    public RepoTest()
    {
        var connectionString = Environment.GetEnvironmentVariable("GRAN_DT_TEST_CONNECTION_STRING");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Define GRAN_DT_TEST_CONNECTION_STRING con la conexión a la base de datos de pruebas.");
        }

        Conexion = new MySqlConnection(connectionString);
    }
}