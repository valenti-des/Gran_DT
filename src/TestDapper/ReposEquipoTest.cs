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
    public void AgregarYObtenerPorId_DeberiaGuardarEquipoConNombreNormalizado()
    {
        var nombre = GenerarNombreEquipo("Equipo_Test");
        var equipo = new Equipo { nombre = $"  {nombre}  " };

        try
        {
            var agregado = _repo.Agregar(equipo);

            Assert.NotEqual((byte)0, agregado.idEquipo);
            Assert.Equal(nombre, agregado.nombre);

            var encontrado = _repo.ObtenerPorId(agregado.idEquipo);
            Assert.NotNull(encontrado);
            Assert.Equal(agregado.idEquipo, encontrado!.idEquipo);
        }
        finally
        {
            if (equipo.idEquipo != 0)
                _repo.Eliminar(equipo.idEquipo);
        }
    }

    [Fact]
    public void Eliminar_DeberiaBorrarEquipoPorId()
    {
        var equipo = new Equipo { nombre = GenerarNombreEquipo("Equipo_Eliminar") };

        try
        {
            _repo.Agregar(equipo);

            Assert.True(_repo.Eliminar(equipo.idEquipo));
            Assert.Null(_repo.ObtenerPorId(equipo.idEquipo));
        }
        finally
        {
            if (equipo.idEquipo != 0)
                _repo.Eliminar(equipo.idEquipo);
        }
    }

}