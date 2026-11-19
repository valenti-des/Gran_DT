using System.Data;
using Dapper;
using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Infraestructura;

public sealed class PuntuacionRepositorio(IDbConnection db) : IPuntuacionRepositorio
{
    public async Task<Puntuacion> CrearAsync(long idFutbolista, decimal valor, byte nroFecha, CancellationToken ct = default)
    {
        var id = await db.ExecuteScalarAsync<long>(new CommandDefinition("""
            INSERT INTO Puntuacion (idFutbolista, puntuacion, cantFech, jornada)
            VALUES (@idFutbolista, @valor, DATE_ADD('2000-01-01', INTERVAL @nroFecha DAY), @nroFecha);
            SELECT LAST_INSERT_ID();
            """, new { idFutbolista, valor, nroFecha }, cancellationToken: ct));
        return new Puntuacion(id, idFutbolista, valor, nroFecha);
    }

    public async Task<IReadOnlyList<Puntuacion>> ListarDeFutbolistaAsync(long idFutbolista, CancellationToken ct = default)
    {
        var rows = await db.QueryAsync<PuntuacionFila>(new CommandDefinition("""
            SELECT idPuntuacion AS IdPuntuacion, idFutbolista AS IdFutbolista, puntuacion AS Valor, jornada AS NroFecha
            FROM Puntuacion WHERE idFutbolista = @idFutbolista ORDER BY jornada
            """, new { idFutbolista }, cancellationToken: ct));
        return rows.Select(r => new Puntuacion(r.IdPuntuacion, r.IdFutbolista, r.Valor, r.NroFecha)).ToArray();
    }

    public Task<bool> ExisteEnFechaAsync(long idFutbolista, byte nroFecha, CancellationToken ct = default) =>
        db.ExecuteScalarAsync<bool>(new CommandDefinition(
            "SELECT EXISTS(SELECT 1 FROM Puntuacion WHERE idFutbolista = @idFutbolista AND jornada = @nroFecha)",
            new { idFutbolista, nroFecha }, cancellationToken: ct));

    private sealed class PuntuacionFila
    {
        public long IdPuntuacion { get; set; }
        public long IdFutbolista { get; set; }
        public decimal Valor { get; set; }
        public byte NroFecha { get; set; }
    }
}