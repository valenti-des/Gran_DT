using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

[Trait("Category", "Integration")]
public class ReposFutbolistaTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoFutbolista _repo;
    private readonly Idbconection _db;
    private ushort _idFutbolistaCreado;

    public ReposFutbolistaTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion()!;
        _repo = new RepoFutbolista(_conexion);
    }

    public void Dispose()
    {
        if (_idFutbolistaCreado != 0)
            _repo.Eliminar(_idFutbolistaCreado);

        _db.CerrarConexion();
    }

    [Fact]
    public void ObtenerTodos_DeberiaRetornarJugadoresExistentes()
    {
        var jugadores = _repo.ObtenerTodos();

        Assert.NotEmpty(jugadores);
        Assert.Contains(jugadores, jugador => jugador.nombre == "Franco" && jugador.apellido == "Armani");
    }

    [Fact]
    public void AgregarYObtenerPorId_DeberiaGuardarJugador()
    {
        var nombre = "Jugador_Test";
        var futbolista = new Futbolista
        {
            nombre = nombre,
            apellido = "Test",
            apodo = "Prueba",
            fechaNac = new DateOnly(2000, 01, 15),
            cotizacion = 15000000m,
            idEquipo = 1,
            idTipoFutbolista = 1
        };

        var agregado = _repo.Agregar(futbolista);
        _idFutbolistaCreado = agregado.idFutbolista;

        Assert.NotEqual((ushort)0, agregado.idFutbolista);
        Assert.Equal(nombre, agregado.nombre);

        var encontrado = _repo.ObtenerPorId(agregado.idFutbolista);
        Assert.NotNull(encontrado);
        Assert.Equal(agregado.idFutbolista, encontrado!.idFutbolista);
    }

    [Fact]
    public void Eliminar_DeberiaBorrarJugadorPorId()
    {
        var futbolista = new Futbolista
        {
            nombre = "Jugador_Eliminar",
            apellido = "Delete",
            apodo = "Borrar",
            fechaNac = new DateOnly(1999, 06, 10),
            cotizacion = 5000000m,
            idEquipo = 1,
            idTipoFutbolista = 1
        };

        var agregado = _repo.Agregar(futbolista);
        _idFutbolistaCreado = agregado.idFutbolista;

        Assert.True(_repo.Eliminar(agregado.idFutbolista));
        Assert.Null(_repo.ObtenerPorId(agregado.idFutbolista));
    }
}