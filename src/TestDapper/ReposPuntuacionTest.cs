using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

[Trait("Category", "Integration")]
public class ReposPuntuacionTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoPuntuacion _repo;
    private readonly Idbconection _db;
    private uint _idPuntuacionCreada;

    public ReposPuntuacionTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion()!;
        _repo = new RepoPuntuacion(_conexion);
    }

    public void Dispose()
    {
        if (_idPuntuacionCreada != 0)
            _repo.Eliminar(_idPuntuacionCreada);

        _db.CerrarConexion();
    }

    [Fact]
    public void ObtenerTodos_DeberiaRetornarPuntuacionesExistentes()
    {
        var puntuaciones = _repo.ObtenerTodos();

        Assert.NotEmpty(puntuaciones);
        Assert.Contains(puntuaciones, puntuacion =>
            puntuacion.idFutbolista == 1 &&
            puntuacion.numFech == 1 &&
            puntuacion.puntuacion == 8.4m);
    }

    [Fact]
    public void AgregarYObtenerPorId_DeberiaGuardarPuntuacion()
    {
        var puntuacion = CrearPuntuacionDePrueba();

        var agregada = _repo.Agregar(puntuacion);
        _idPuntuacionCreada = agregada.idPuntuacion;

        Assert.NotEqual(0u, agregada.idPuntuacion);

        var encontrada = _repo.ObtenerPorId(agregada.idPuntuacion);

        Assert.NotNull(encontrada);
        Assert.Equal(agregada.idPuntuacion, encontrada!.idPuntuacion);
        Assert.Equal(agregada.idFutbolista, encontrada.idFutbolista);
        Assert.Equal(agregada.numFech, encontrada.numFech);
        Assert.Equal(agregada.puntuacion, encontrada.puntuacion);
    }

    [Fact]
    public void Eliminar_DeberiaBorrarPuntuacionPorId()
    {
        var puntuacion = CrearPuntuacionDePrueba();

        var agregada = _repo.Agregar(puntuacion);
        _idPuntuacionCreada = agregada.idPuntuacion;

        Assert.True(_repo.Eliminar(agregada.idPuntuacion));
        Assert.Null(_repo.ObtenerPorId(agregada.idPuntuacion));
    }

    private Puntuacion CrearPuntuacionDePrueba()
    {
        const ushort idFutbolistaExistente = 1;

        return new Puntuacion
        {
            idFutbolista = idFutbolistaExistente,
            numFech = 2,
            puntuacion = 8.4m
        };
    }
}
