using System.Data;
using System.Data.Common;
using Dapper;
using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Infraestructura;

public sealed class FutbolistaRepositorio(IDbConnection db, IPuntuacionRepositorio puntuaciones) : IFutbolistaRepositorio
{
    private const string SelectSql = """
        SELECT idFutbolista AS IdFutbolista, nombre AS Nombre, apellido AS Apellido, apodo AS Apodo,
               fechaNac AS FechaNac, cotizacion AS Cotizacion, idEquipo AS IdEquipo, idTipoFutbolista AS Tipo
        FROM Futbolista
        """;

    public async Task<IReadOnlyList<Futbolista>> ListarAsync(CancellationToken ct = default)
    {
        var rows = await db.QueryAsync<FutbolistaFila>(new CommandDefinition(SelectSql + " ORDER BY apellido, nombre", cancellationToken: ct));
        var resultado = new List<Futbolista>();
        foreach (var row in rows) resultado.Add(await MapearAsync(row, ct));
        return resultado;
    }

    public async Task<Futbolista?> ObtenerAsync(long id, CancellationToken ct = default)
    {
        var row = await db.QuerySingleOrDefaultAsync<FutbolistaFila>(new CommandDefinition(
            SelectSql + " WHERE idFutbolista = @id", new { id }, cancellationToken: ct));
        return row is null ? null : await MapearAsync(row, ct);
    }

    public async Task<Futbolista> CrearAsync(Futbolista futbolista,
        IReadOnlyCollection<(decimal Valor, byte NroFecha)> nuevasPuntuaciones, CancellationToken ct = default)
    {
        var conexion = (DbConnection)db;
        await conexion.OpenAsync(ct);
        await using var transaccion = await conexion.BeginTransactionAsync(ct);
        try
        {
            var id = await db.ExecuteScalarAsync<long>(new CommandDefinition("""
                INSERT INTO Futbolista (nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista)
                VALUES (@Nombre, @Apellido, @Apodo, @FechaNac, @Cotizacion, @IdEquipo, @Tipo);
                SELECT LAST_INSERT_ID();
                """, futbolista, transaccion, cancellationToken: ct));
            foreach (var puntuacion in nuevasPuntuaciones)
            {
                await db.ExecuteAsync(new CommandDefinition("""
                    INSERT INTO Puntuacion (idFutbolista, puntuacion, cantFech, jornada)
                    VALUES (@id, @valor, DATE_ADD('2000-01-01', INTERVAL @fecha DAY), @fecha)
                    """, new { id, valor = puntuacion.Valor, fecha = puntuacion.NroFecha }, transaccion, cancellationToken: ct));
            }
            await transaccion.CommitAsync(ct);
            return await ObtenerAsync(id, ct) ?? throw new InvalidOperationException("No se pudo recuperar el futbolista creado.");
        }
        catch
        {
            await transaccion.RollbackAsync(ct);
            throw;
        }
    }

    public async Task<Futbolista?> ActualizarAsync(Futbolista futbolista, CancellationToken ct = default)
    {
        var filas = await db.ExecuteAsync(new CommandDefinition("""
            UPDATE Futbolista SET nombre = @Nombre, apellido = @Apellido, apodo = @Apodo, fechaNac = @FechaNac,
                cotizacion = @Cotizacion, idEquipo = @IdEquipo, idTipoFutbolista = @Tipo
            WHERE idFutbolista = @IdFutbolista
            """, futbolista, cancellationToken: ct));
        return filas == 0 ? null : await ObtenerAsync(futbolista.IdFutbolista, ct);
    }

    private async Task<Futbolista> MapearAsync(FutbolistaFila row, CancellationToken ct)
    {
        var scores = await puntuaciones.ListarDeFutbolistaAsync(row.IdFutbolista, ct);
        return new Futbolista(row.IdFutbolista, row.Nombre, row.Apellido, row.Apodo, row.FechaNac,
            row.Cotizacion, row.IdEquipo, (TipoFutbolista)row.Tipo, scores);
    }

    private sealed class FutbolistaFila
    {
        public long IdFutbolista { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public string? Apodo { get; set; }
        public DateOnly? FechaNac { get; set; }
        public decimal Cotizacion { get; set; }
        public long IdEquipo { get; set; }
        public byte Tipo { get; set; }
    }
}