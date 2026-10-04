using System;
using Biblio;
using Biblio.BiblioConDapper;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

public class ReposUsuarioTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoUsuario _repo;
    private readonly Idbconection _db;

    public ReposUsuarioTest()
    {
        _db = new Idbconection();
        _conexion = _db.EstablecerConexion() ?? throw new InvalidOperationException("No se pudo abrir la conexión a la base de datos.");
        _repo = new RepoUsuario(_conexion);
    }

    private static string GenerarEmailUsuario(string prefijo)
    {
        var email = $"{prefijo}_{Guid.NewGuid():N}@test.com";
        return email.Length > 255 ? email[..255] : email;
    }

    public void Dispose() => _db.CerrarConexion();

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
        var email = GenerarEmailUsuario("usuario");
        var usuario = new Usuario
        {
            nombre = "Usuario",
            apellido = "Test",
            email = email,
            fechaNac = new DateOnly(1998, 04, 12),
            contraseña = "abc123",
            es_admin = false
        };

        try
        {
            var agregado = _repo.Agregar(usuario);

            Assert.NotEqual((short)0, agregado.idUsuario);
            Assert.Equal(email, agregado.email);

            var encontrado = _repo.ObtenerPorEmail(email);
            Assert.NotNull(encontrado);
            Assert.Equal(agregado.idUsuario, encontrado!.idUsuario);
        }
        finally
        {
            _repo.Eliminar(email);
        }
    }
}
