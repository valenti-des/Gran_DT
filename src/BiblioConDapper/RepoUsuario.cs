using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoUsuario : RepoDapper, IRepoUsuario
{
    public RepoUsuario(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Usuario> ObtenerTodos() =>
        Conexion.Query<Usuario>(@"
            SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
            FROM Usuario").AsList();

    public Usuario? ObtenerPorId(ushort id) =>
        Conexion.QuerySingleOrDefault<Usuario>(@"
            SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
            FROM Usuario
            WHERE idUsuario = @Id
            LIMIT 1;", new { Id = id });

    public Usuario? ObtenerPorEmail(string email)
    {
        return Conexion.QuerySingleOrDefault<Usuario>(@"
            SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
            FROM Usuario
            WHERE email = @Email
            LIMIT 1;", new { Email = email });
    }

    public Usuario Agregar(Usuario usuario)
    {
        var id = Conexion.ExecuteScalar<ushort>(@"
            INSERT INTO Usuario (nombre, apellido, email, fechaNac, contraseña, es_admin)
            VALUES (@Nombre, @Apellido, @Email, @FechaNac, @Contrasena, @EsAdmin);
            SELECT LAST_INSERT_ID();", new
        {
            Nombre = usuario.nombre,
            Apellido = usuario.apellido,
            Email = usuario.email,
            FechaNac = usuario.fechaNac,
            Contrasena = usuario.contraseña,
            EsAdmin = usuario.es_admin
        });

        usuario.idUsuario = id;
        return usuario;
    }

    public bool Eliminar(ushort id) =>
        Conexion.Execute("DELETE FROM Usuario WHERE idUsuario = @Id", new { Id = id }) > 0;
}