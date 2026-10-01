using System.Data;
using MySqlConnector;

namespace Test;

/// <summary>
/// Provee una conexión MySql para tests de integración locales.
/// Usa la cadena por defecto o puede recibir una diferente.
/// </summary>
public class TestRepo
{
    protected readonly IDbConnection _conexion;

    public TestRepo() => _conexion = new MySqlConnection(BuildConnectionString());
    public TestRepo(string cadena) => _conexion = new MySqlConnection(cadena);

    private static string BuildConnectionString()
    {
        var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "bd_gran_dt";
        var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "12345";

        return $"Server={server};Port={port};User ID={user};Password={password};Database={database};";
    }
}