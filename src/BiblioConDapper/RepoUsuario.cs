using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoUsuario : RepoDapper, IRepoUsuario
{
    private const string LoginSql = @"
        SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
        FROM Usuario
        WHERE email = @Email AND contraseña = @Contrasena
        LIMIT 1;";

    private const string UpdateSql = @"
        UPDATE Usuario
        SET nombre = @Nombre,
            apellido = @Apellido,
            email = @Email,
            fechaNac = @FechaNac,
            contraseña = @Contrasena,
            es_admin = @EsAdmin
        WHERE idUsuario = @IdUsuario;";

    public RepoUsuario(IDbConnection conexion) : base(conexion)
    {
    }

    public int AltaUsuario(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (string.IsNullOrWhiteSpace(usuario.nombre))
            throw new ArgumentException("El nombre es obligatorio.", nameof(usuario));

        if (string.IsNullOrWhiteSpace(usuario.apellido))
            throw new ArgumentException("El apellido es obligatorio.", nameof(usuario));

        if (string.IsNullOrWhiteSpace(usuario.email))
            throw new ArgumentException("El email es obligatorio.", nameof(usuario));

        if (string.IsNullOrWhiteSpace(usuario.contraseña))
            throw new ArgumentException("La contraseña es obligatoria.", nameof(usuario));

        var sql = @"
            INSERT INTO Usuario (nombre, apellido, email, fechaNac, contraseña, es_admin)
            VALUES (@Nombre, @Apellido, @Email, @FechaNac, @Contrasena, @EsAdmin);
            SELECT LAST_INSERT_ID();";

        return Conexion.ExecuteScalar<short>(sql, new
        {
            Nombre = usuario.nombre.Trim(),
            Apellido = usuario.apellido.Trim(),
            Email = usuario.email.Trim(),
            FechaNac = usuario.fechaNac,
            Contrasena = usuario.contraseña,
            EsAdmin = usuario.es_admin
        });
    }

    public int ModificarUsuario(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        return Conexion.Execute(UpdateSql, new
        {
            IdUsuario = usuario.idUsuario,
            Nombre = usuario.nombre.Trim(),
            Apellido = usuario.apellido.Trim(),
            Email = usuario.email.Trim(),
            FechaNac = usuario.fechaNac,
            Contrasena = usuario.contraseña,
            EsAdmin = usuario.es_admin
        });
    }

    public int EliminarUsuario(int idUsuario) =>
        Conexion.Execute("DELETE FROM Usuario WHERE idUsuario = @IdUsuario", new { IdUsuario = idUsuario });

    public List<Usuario> ObtenerTodos() =>
        Conexion.Query<Usuario>(@"
            SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
            FROM Usuario").AsList();

    public Usuario? ObtenerPorEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        return Conexion.QuerySingleOrDefault<Usuario>(@"
            SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
            FROM Usuario
            WHERE email = @Email
            LIMIT 1;", new { Email = email.Trim() });
    }

    public Usuario? ObtenerPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return null;

        return Conexion.QuerySingleOrDefault<Usuario>(@"
            SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
            FROM Usuario
            WHERE nombre = @Nombre
            LIMIT 1;", new { Nombre = nombre.Trim() });
    }

    public Usuario Agregar(Usuario usuario)
    {
        var id = AltaUsuario(usuario);
        usuario.idUsuario = (short)id;
        return usuario;
    }

    public bool Eliminar(string email) =>
        Conexion.Execute("DELETE FROM Usuario WHERE email = @Email", new { Email = email }) > 0;

    public Usuario? LoginUsuario(string email, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(contrasena))
            return null;

        return Conexion.QuerySingleOrDefault<Usuario>(LoginSql, new
        {
            Email = email.Trim(),
            Contrasena = contrasena
        });
    }
}