using System.Data;
using Dapper;
using GranDT.Aplicacion.IRepo;

namespace GranDT.Infraestructura;

public sealed class PlantillaFutbolistaRepositorio(IDbConnection db) : IPlantillaFutbolistaRepositorio
{
    public Task<bool> ExisteAsync(long idPlantilla, long idFutbolista, CancellationToken ct = default) =>
        db.ExecuteScalarAsync<bool>(new CommandDefinition(
            "SELECT EXISTS(SELECT 1 FROM Futbolista_Plantilla WHERE idPlantilla = @idPlantilla AND idFutbolista = @idFutbolista)",
            new { idPlantilla, idFutbolista }, cancellationToken: ct));

    public Task<int> ContarAsync(long idPlantilla, CancellationToken ct = default) =>
        db.ExecuteScalarAsync<int>(new CommandDefinition(
            "SELECT COUNT(*) FROM Futbolista_Plantilla WHERE idPlantilla = @idPlantilla", new { idPlantilla }, cancellationToken: ct));

    public Task AgregarAsync(long idPlantilla, long idFutbolista, bool titular, CancellationToken ct = default) =>
        db.ExecuteAsync(new CommandDefinition("""
            INSERT INTO Futbolista_Plantilla (futbolistaTitular, validacionP, validacionR, idPlantilla, idFutbolista)
            VALUES (@titular, 0, 0, @idPlantilla, @idFutbolista)
            """, new { idPlantilla, idFutbolista, titular }, cancellationToken: ct));

    public async Task<bool> CambiarCondicionAsync(long idPlantilla, long idFutbolista, bool titular, CancellationToken ct = default) =>
        await db.ExecuteAsync(new CommandDefinition("""
            UPDATE Futbolista_Plantilla SET futbolistaTitular = @titular
            WHERE idPlantilla = @idPlantilla AND idFutbolista = @idFutbolista
            """, new { idPlantilla, idFutbolista, titular }, cancellationToken: ct)) > 0;
}