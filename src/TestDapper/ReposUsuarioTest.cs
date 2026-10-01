using System;
using Biblio;
using BiblioConDapper;
using MySqlConnector;

namespace TestDapper;

public class ReposUsuarioTest : IDisposable
{
    private readonly MySqlConnection _conexion;
    private readonly RepoUsuario _repo;

    public ReposUsuarioTest()
    {
        _conexion = new MySqlConnection(BuildConnectionString());
        _conexion.Open();
        _repo = new RepoUsuario(_conexion);
    }

    public void Dispose() => _conexion.Dispose();

    private static string BuildConnectionString()
    {
        var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost";
        var port = Environment.GetEnvironmentVariable("DB_PORT") ?? "3306";
        var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "bd_gran_dt";
        var user = Environment.GetEnvironmentVariable("DB_USER") ?? "root";
        var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "Trigg3rs!";

        return $"Server={server};Port={port};User ID={user};Password={password};Database={database};";
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
        var email = $"usuario_{Guid.NewGuid():N}@test.com";
        var usuario = new Usuario
        {
            nombre = "Usuario",
            apellido = "Test",
            email = email,
            fechaNac = new DateOnly(1998, 04, 12),
            contraseña = "abc123",
            es_admin = false
        };

        var agregado = _repo.Agregar(usuario);

        Assert.NotEqual((short)0, agregado.idUsuario);
        Assert.Equal(email, agregado.email);

        var encontrado = _repo.ObtenerPorEmail(email);
        Assert.NotNull(encontrado);
        Assert.Equal(agregado.idUsuario, encontrado!.idUsuario);

        Assert.True(_repo.Eliminar(email));
    }
}
