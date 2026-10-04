using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

public class ReposFutbolistaTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoFutbolista _repo;
    private readonly Idbconection _db;

    public ReposFutbolistaTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion() ?? throw new InvalidOperationException("No se pudo abrir la conexión a la base de datos.");
        _repo = new RepoFutbolista(_conexion);
    }

    private static string GenerarNombreFutbolista(string prefijo)
    {
        var nombre = $"{prefijo}_{Guid.NewGuid():N}";
        return nombre.Length > 45 ? nombre[..45] : nombre;
    }

    public void Dispose() => _db.CerrarConexion();

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
        var nombre = GenerarNombreFutbolista("Jugador");
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

        try
        {
            var agregado = _repo.Agregar(jugador);

            Assert.NotEqual((ushort)0, agregado.idFutbolista);
            Assert.Equal(nombre, agregado.nombre);

            var encontrado = _repo.ObtenerPorId(agregado.idFutbolista);
            Assert.NotNull(encontrado);
            Assert.Equal(agregado.idFutbolista, encontrado!.idFutbolista);
        }
        finally
        {
            _repo.Eliminar(jugador.nombre);
        }
    }
}
