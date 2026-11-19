using System.Data;
using Dapper;
using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Infraestructura;

public sealed class EquipoRepositorio(IDbConnection db) : IEquipoRepositorio
{
    public async Task<IReadOnlyList<Equipo>> ListarAsync(CancellationToken ct = default) =>
        (await db.QueryAsync<Equipo>(new CommandDefinition(
            "SELECT idEquipo AS IdEquipo, nombre AS Nombre FROM Equipo ORDER BY nombre", cancellationToken: ct))).AsList();

    public async Task<bool> ExisteAsync(long id, CancellationToken ct = default) =>
        await db.ExecuteScalarAsync<bool>(new CommandDefinition(
            "SELECT EXISTS(SELECT 1 FROM Equipo WHERE idEquipo = @id)", new { id }, cancellationToken: ct));
}