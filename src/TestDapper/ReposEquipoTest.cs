using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

[Trait("Category", "Integration")]
public class ReposEquipoTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoEquipo _repo;
    private readonly Idbconection _db;

    public ReposEquipoTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion() ?? throw new InvalidOperationException("No se pudo abrir la conexión a la base de datos.");
        _repo = new RepoEquipo(_conexion);
    }

    private static string GenerarNombreEquipo(string prefijo)
    {
        var nombre = $"{prefijo}_{Guid.NewGuid():N}";
        return nombre.Length > 45 ? nombre[..45] : nombre;
    }

    public void Dispose() => _db.CerrarConexion();

    [Fact]
    public void ObtenerTodos_DeberiaRetornarEquiposExistentes()
    {
        var equipos = _repo.ObtenerTodos();

        Assert.NotEmpty(equipos);
        Assert.Contains(equipos, equipo => equipo.nombre == "River Plate");
    }

    [Fact]
    public void AgregarYObtenerPorNombre_DeberiaGuardarEquipoConNombreNormalizado()
    {
        var nombre = GenerarNombreEquipo("Equipo_Test");
        var equipo = new Equipo { nombre = $"  {nombre}  " };

        try
        {
            var agregado = _repo.Agregar(equipo);

            Assert.NotEqual((byte)0, agregado.idEquipo);
            Assert.Equal(nombre, agregado.nombre);

            var encontrado = _repo.ObtenerPorNombre(nombre);
            Assert.NotNull(encontrado);
            Assert.Equal(agregado.idEquipo, encontrado!.idEquipo);
        }
        finally
        {
            _repo.Eliminar(nombre);
        }
    }

    [Fact]
    public void Eliminar_DeberiaBorrarEquipoPorNombre()
    {
        var nombre = GenerarNombreEquipo("Equipo_Eliminar");

        try
        {
            _repo.Agregar(new Equipo { nombre = nombre });

            Assert.True(_repo.Eliminar(nombre));
            Assert.Null(_repo.ObtenerPorNombre(nombre));
        }
        finally
        {
            _repo.Eliminar(nombre);
        }
    }

    [Fact]
    public void RepoFutbolista_ObtenerPorId_DeberiaCargarPuntuacionesDelJugador()
    {
        var repo = new RepoFutbolista(_conexion);

        var futbolista = repo.ObtenerPorId(1);

        Assert.NotNull(futbolista);
        Assert.NotEmpty(futbolista!.puntuaciones);
        Assert.Equal(8.4f, futbolista.ObtenerPuntuacionPorFecha(1));
    }

    [Fact]
    public void RepoPlantilla_ObtenerPorId_DeberiaCargarDetallesYCalcularPuntajeFecha()
    {
        var repo = new RepoPlantilla(_conexion);

        var plantilla = repo.ObtenerPorId(1);

        Assert.NotNull(plantilla);
        Assert.NotEmpty(plantilla!.detalles);
        Assert.Equal(8.4f, plantilla.PuntajeFecha(1));
    }
}