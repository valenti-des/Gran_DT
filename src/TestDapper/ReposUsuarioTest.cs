using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

[Trait("Category", "Integration")]
public class ReposUsuarioTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoUsuario _repo;
    private readonly Idbconection _db;
    private ushort _idUsuarioCreado;

    public ReposUsuarioTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion()!;
        _repo = new RepoUsuario(_conexion);
    }

    public void Dispose()
    {
        if (_idUsuarioCreado != 0)
            _repo.Eliminar(_idUsuarioCreado);

        _db.CerrarConexion();
    }

    [Fact]
    public void ObtenerTodos_DeberiaRetornarUsuariosExistentes()
    {
        var usuarios = _repo.ObtenerTodos();

        Assert.NotEmpty(usuarios);
        Assert.Contains(usuarios, usuario => usuario.email == "admin@gran_dt.com");
    }

    [Fact]
    public void AgregarYObtenerPorEmail_DeberiaGuardarUsuario()
    {
        var email = "usuario_test@gran_dt.com";
        var usuario = new Usuario
        {
            nombre = "Usuario",
            apellido = "Test",
            email = email,
            fechaNac = new DateOnly(1998, 04, 12),
            contrasena = "abc123",
            es_admin = false
        };

        var agregado = _repo.Agregar(usuario);
        _idUsuarioCreado = agregado.idUsuario;

        Assert.NotEqual((ushort)0, agregado.idUsuario);
        Assert.Equal(email, agregado.email);

        var encontrado = _repo.ObtenerPorEmail(email);
        Assert.NotNull(encontrado);
        Assert.Equal(agregado.idUsuario, encontrado!.idUsuario);
    }

    [Fact]
    public void Eliminar_DeberiaBorrarUsuarioPorId()
    {
        var email = "usuario_eliminar@gran_dt.com";
        var usuario = new Usuario
        {
            nombre = "Usuario",
            apellido = "Delete",
            email = email,
            fechaNac = new DateOnly(1995, 08, 22),
            contrasena = "secret123",
            es_admin = false
        };

        var agregado = _repo.Agregar(usuario);
        _idUsuarioCreado = agregado.idUsuario;

        Assert.True(_repo.Eliminar(agregado.idUsuario));
        Assert.Null(_repo.ObtenerPorEmail(email));
    }
}
