using System.Data;
using Biblio;
using BiblioConDapper.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoUsuario : RepoDapper, IRepoUsuario
{
    private const string InsertSql = @"
        INSERT INTO Usuario (nombre, apellido, email, fechaNac, contraseña, es_admin)
        VALUES (@Nombre, @Apellido, @Email, @FechaNac, @Contrasena, @EsAdmin);
        SELECT LAST_INSERT_ID();";

    private const string LoginSql = @"
        SELECT idUsuario, nombre, apellido, email, fechaNac, contraseña, es_admin
        FROM Usuario
        WHERE email = @Email AND contraseña = @Contrasena
        LIMIT 1;";

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

        var id = Conexion.QuerySingle<int>(InsertSql, new
        {
            Nombre = usuario.nombre.Trim(),
            Apellido = usuario.apellido.Trim(),
            Email = usuario.email.Trim(),
            FechaNac = usuario.fechaNac,
            Contrasena = usuario.contraseña,
            EsAdmin = usuario.es_admin
        });

        return id;
    }

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