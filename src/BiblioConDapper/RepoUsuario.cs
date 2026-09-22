using System.Data;
using Biblio;
using BiblioConDapper.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoUsuario : RepoDapper, IRepoUsuario
{
    public RepoUsuario(IDbConnection conexion)
        : base(conexion)
    {
    }

    public void AltaUsuario(Usuario usuario, string contraseña)
    {
        const string sql = @"
            INSERT INTO Usuario (nombre, apellido, email, fechaNac, contraseña, es_admin)
            VALUES (@nombre, @apellido, @email, @fechaNac, @contraseña, @es_admin);";

        _conexion.Execute(sql, new
        {
            nombre = usuario.nombre,
            apellido = usuario.apellido,
            email = usuario.email,
            fechaNac = usuario.fechaNac,
            contraseña,
            es_admin = usuario.es_admin
        });
    }

    public Usuario? UsuarioPorContraseña(string email, string contraseña)
    {
        const string sql = @"
            SELECT
                idUsuario,
                nombre,
                apellido,
                email,
                fechaNac,
                contraseña,
                es_admin
            FROM Usuario
            WHERE email = @email AND contraseña = @contraseña;";

        return _conexion.QuerySingleOrDefault<Usuario>(sql, new { email, contraseña });
    }
}