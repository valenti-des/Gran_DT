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
        _conexion = _db.EstablecerConexion();
        _repo = new RepoPlantilla(_conexion);
    }

    public void Dispose() => _db.CerrarConexion();

    [Fact]
    public void ObtenerTodos_DeberiaRetornarPlantillasExistentes()
    {
        var plantillas = _repo.ObtenerTodos();

        Assert.NotEmpty(plantillas);
        Assert.Contains(plantillas, plantilla => plantilla.nombreP == "Plantilla Inicial");
    }

    [Fact]
    public void ObtenerPorId_DeberiaCargarDetallesYCalcularPuntajeDeLaJornada()
    {
        var plantilla = _repo.ObtenerPorId(1);

        Assert.NotNull(plantilla);
        Assert.NotEmpty(plantilla!.detalles);
        Assert.Equal(8.4m, plantilla.PuntajeFecha(1));
    }

    [Fact]
    public void Eliminar_DeberiaBorrarPlantillaPorId()
    {
        var plantilla = new Plantilla
        {
            nombreP = GenerarNombrePlantilla("Plantilla_Eliminar"),
            cantMaxMonto = 1000000m,
            cantMaxFutbolista = 11,
            idUsuario = 1
        };

        _repo.Agregar(plantilla);

        Assert.True(_repo.Eliminar(plantilla.idPlantilla));
        Assert.Null(_repo.ObtenerPorId(plantilla.idPlantilla));
    }
}
