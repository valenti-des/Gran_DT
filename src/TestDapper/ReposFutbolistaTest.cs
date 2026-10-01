using System;
using Biblio;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

public class ReposFutbolistaTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoFutbolista _repo;

    public ReposFutbolistaTest()
    {
        _conexion = new MySqlConnection(BuildConnectionString());
        _conexion.Open();
        _repo = new RepoFutbolista(_conexion);
    }

    public void Dispose() => _conexion.Dispose();

    private static string BuildConnectionString()
    {
        var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "127.0.0.1";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "bd_gran_dt";
        var user = Environment.GetEnvironmentVariable("DB_USER") ?? "5to_agbd";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Trigg3rs!";

        return $"Server={server};Port={port};User ID={user};Password={password};Database={database};";
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
        var nombre = $"Jugador_{Guid.NewGuid():N}";
        var jugador = new Futbolista
        {
            nombre = nombre,
            apellido = "Test",
            apodo = "Prueba",
            fechaNac = new DateOnly(2000, 01, 15),
            cotizacion = 15000000m,
            idEquipo = 1,
            idTipoFutbolista = 1
        };

        var agregado = _repo.Agregar(jugador);

        Assert.NotEqual((ushort)0, agregado.idFutbolista);
        Assert.Equal(nombre, agregado.nombre);

        var encontrado = _repo.ObtenerPorId(agregado.idFutbolista);
        Assert.NotNull(encontrado);
        Assert.Equal(agregado.idFutbolista, encontrado!.idFutbolista);

        Assert.True(_repo.Eliminar(agregado.idFutbolista));
    }
}
