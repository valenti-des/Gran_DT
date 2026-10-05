using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

[Trait("Category", "Integration")]
public class ReposPlantillaTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoPlantilla _repo;
    private readonly Idbconection _db;

    public ReposPlantillaTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion() ?? throw new InvalidOperationException("No se pudo abrir la conexión a la base de datos.");
        _repo = new RepoPlantilla(_conexion);
    }

    public void Dispose() => _db.CerrarConexion();

    private static string GenerarNombrePlantilla(string prefijo)
    {
        var nombre = $"{prefijo}_{Guid.NewGuid():N}";
        return nombre.Length > 45 ? nombre[..45] : nombre;
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
        Assert.Equal(8.4f, plantilla.PuntajeFecha(new DateOnly(2026, 1, 1)));
    }
}
