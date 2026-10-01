using System;
using Biblio;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

public class ReposPlantillaTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoPlantilla _repo;

    public ReposPlantillaTest()
    {
        _conexion = new MySqlConnection(BuildConnectionString());
        _conexion.Open();
        _repo = new RepoPlantilla(_conexion);
    }

    public void Dispose() => _conexion.Dispose();

    private static string GenerarNombrePlantilla(string prefijo)
    {
        var nombre = $"{prefijo}_{Guid.NewGuid():N}";
        return nombre.Length > 45 ? nombre[..45] : nombre;
    }

    private static string BuildConnectionString()
    {
        var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "bd_gran_dt";
        var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "12345";

        return $"Server={server};Port={port};User ID={user};Password={password};Database={database};";
    }

    [Fact]
    public void ObtenerTodos_DeberiaRetornarPlantillasExistentes()
    {
        var plantillas = _repo.ObtenerTodos();

        Assert.NotEmpty(plantillas);
        Assert.Contains(plantillas, plantilla => plantilla.nombreP == "Plantilla Inicial");
    }

    [Fact]
    public void ObtenerPorId_DeberiaCargarDetallesYCalcularPuntajeFecha()
    {
        var plantilla = _repo.ObtenerPorId(1);

        Assert.NotNull(plantilla);
        Assert.NotEmpty(plantilla!.detalles);
        Assert.Equal(8.4f, plantilla.PuntajeFecha(1));
    }
}
