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

    public ReposPuntuacionTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion();
        _repo = new RepoPuntuacion(_conexion);
    }

    public void Dispose() => _db.CerrarConexion();

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

        _repo.Agregar(puntuacion);

        Assert.NotEqual(0u, puntuacion.idPuntuacion);

        var encontrada = _repo.ObtenerPorId(puntuacion.idPuntuacion);

        Assert.NotNull(encontrada);
        Assert.Equal(puntuacion.idPuntuacion, encontrada!.idPuntuacion);
        Assert.Equal(puntuacion.idFutbolista, encontrada.idFutbolista);
        Assert.Equal(puntuacion.numFech, encontrada.numFech);
        Assert.Equal(puntuacion.puntuacion, encontrada.puntuacion);
    }

    [Fact]
    public void Eliminar_DeberiaBorrarPuntuacionPorId()
    {
        var puntuacion = CrearPuntuacionDePrueba();

        _repo.Agregar(puntuacion);

        Assert.True(_repo.Eliminar(puntuacion.idPuntuacion));
        Assert.Null(_repo.ObtenerPorId(puntuacion.idPuntuacion));
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
