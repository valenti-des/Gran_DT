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
    private uint _idPlantillaCreada;

    public ReposPlantillaTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion()!;
        _repo = new RepoPlantilla(_conexion);
    }

    public void Dispose()
    {
        if (_idPlantillaCreada != 0)
            _repo.Eliminar(_idPlantillaCreada);

        _db.CerrarConexion();
    }

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
            nombreP = "Plantilla_Eliminar",
            presupuesto = 1000000m,
            cantFutbolista = 11,
            idUsuario = 1
        };

        var agregada = _repo.Agregar(plantilla);
        _idPlantillaCreada = agregada.idPlantilla;

        Assert.True(_repo.Eliminar(agregada.idPlantilla));
        Assert.Null(_repo.ObtenerPorId(agregada.idPlantilla));
    }
}