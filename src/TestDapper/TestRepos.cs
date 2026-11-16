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
    public TestRepo() => _conexion = new Idbconection().EstablecerConexion();
    public TestRepo(string cadena) => _conexion = new MySqlConnection(cadena);
}