using System.Data;
using Dapper;
using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Infraestructura;

public sealed class UsuarioRepositorio(IDbConnection db) : IUsuarioRepositorio
{
    public async Task<Usuario?> ObtenerAsync(long id, CancellationToken ct = default) =>
        await db.QuerySingleOrDefaultAsync<Usuario>(new CommandDefinition("""
            SELECT idUsuario AS IdUsuario, nombre AS Nombre, apellido AS Apellido, email AS Email,
                   fechaNac AS FechaNac, es_admin AS EsAdmin
            FROM Usuario WHERE idUsuario = @id
            """, new { id }, cancellationToken: ct));

    public Task<bool> ExisteEmailAsync(string email, CancellationToken ct = default) =>
        db.ExecuteScalarAsync<bool>(new CommandDefinition(
            "SELECT EXISTS(SELECT 1 FROM Usuario WHERE LOWER(email) = @email)", new { email }, cancellationToken: ct));

    public async Task<Usuario> CrearAsync(string nombre, string apellido, string email, DateOnly? fechaNac,
        string hashContrasena, bool esAdmin, CancellationToken ct = default)
    {
        var id = await db.ExecuteScalarAsync<long>(new CommandDefinition("""
            INSERT INTO Usuario (nombre, apellido, email, fechaNac, `contraseña`, es_admin)
            VALUES (@nombre, @apellido, @email, @fechaNac, @hashContrasena, @esAdmin);
            SELECT LAST_INSERT_ID();
            """, new { nombre, apellido, email, fechaNac, hashContrasena, esAdmin }, cancellationToken: ct));
        return new Usuario(id, nombre, apellido, email, fechaNac, esAdmin);
    }
}