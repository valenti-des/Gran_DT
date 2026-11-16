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
        _conexion = _db.EstablecerConexion();
        _repo = new RepoEquipo(_conexion);
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

        var agregado = _repo.Agregar(equipo);

        Assert.NotEqual((byte)0, agregado.idEquipo);
        Assert.Equal(nombre, agregado.nombre);

        var encontrado = _repo.ObtenerPorId(agregado.idEquipo);
        Assert.NotNull(encontrado);
        Assert.Equal(agregado.idEquipo, encontrado!.idEquipo);
    }

    [Fact]
    public void Eliminar_DeberiaBorrarEquipoPorId()
    {
        var equipo = new Equipo { nombre = GenerarNombreEquipo("Equipo_Eliminar") };

        _repo.Agregar(equipo);

        Assert.True(_repo.Eliminar(equipo.idEquipo));
        Assert.Null(_repo.ObtenerPorId(equipo.idEquipo));
    }

    
}