using System.Data;
using Biblio.BiblioConDapper;
using MySqlConnector;

namespace Test;

/// <summary>
/// Provee una conexión MySql para tests de integración locales.
/// Usa la cadena por defecto o puede recibir una diferente.
/// </summary>
public class TestRepo
{
    protected readonly IDbConnection _conexion;
    public TestRepo() => _conexion = new Idbconection().EstablecerConexion() ?? throw new InvalidOperationException("No se pudo abrir la conexión a la base de datos.");
    public TestRepo(string cadena) => _conexion = new MySqlConnection(cadena);
}