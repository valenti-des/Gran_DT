using System.Data;
using Dapper;
using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Infraestructura;

public sealed class PlantillaRepositorio(IDbConnection db, IFutbolistaRepositorio futbolistas) : IPlantillaRepositorio
{
    public async Task<Plantilla?> ObtenerAsync(long id, CancellationToken ct = default)
    {
        var fila = await db.QuerySingleOrDefaultAsync<PlantillaFila>(new CommandDefinition("""
            SELECT idPlantilla AS IdPlantilla, nombreP AS NombreP, cantMaxMonto AS Presupuesto,
                   cantMaxFutbolista AS CantFutbolista, idUsuario AS IdUsuario
            FROM Plantilla WHERE idPlantilla = @id
            """, new { id }, cancellationToken: ct));
        if (fila is null) return null;
        var ids = await db.QueryAsync<DetalleFila>(new CommandDefinition("""
            SELECT idPlantilla AS IdPlantilla, idFutbolista AS IdFutbolista,
                   futbolistaTitular AS FutbolistaTitular
            FROM Futbolista_Plantilla WHERE idPlantilla = @id ORDER BY idFutbolistaPlantilla
            """, new { id }, cancellationToken: ct));
        var detalles = new List<PlantillaFutbolista>();
        foreach (var detalle in ids)
        {
            var jugador = await futbolistas.ObtenerAsync(detalle.IdFutbolista, ct);
            if (jugador is not null) detalles.Add(new PlantillaFutbolista(id, jugador, detalle.FutbolistaTitular));
        }
        return new Plantilla(fila.IdPlantilla, fila.NombreP, fila.Presupuesto, fila.CantFutbolista, fila.IdUsuario, detalles);
    }

    public async Task<IReadOnlyList<Plantilla>> ListarDeUsuarioAsync(long idUsuario, CancellationToken ct = default)
    {
        var ids = await db.QueryAsync<long>(new CommandDefinition(
            "SELECT idPlantilla FROM Plantilla WHERE idUsuario = @idUsuario ORDER BY idPlantilla", new { idUsuario }, cancellationToken: ct));
        var resultado = new List<Plantilla>();
        foreach (var id in ids)
        {
            var plantilla = await ObtenerAsync(id, ct);
            if (plantilla is not null) resultado.Add(plantilla);
        }
        return resultado;
    }

    public async Task<Plantilla> CrearAsync(string nombre, decimal presupuesto, int cantidad, long idUsuario, CancellationToken ct = default)
    {
        var id = await db.ExecuteScalarAsync<long>(new CommandDefinition("""
            INSERT INTO Plantilla (nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario)
            VALUES (@nombre, @presupuesto, @cantidad, @idUsuario);
            SELECT LAST_INSERT_ID();
            """, new { nombre, presupuesto, cantidad, idUsuario }, cancellationToken: ct));
        return await ObtenerAsync(id, ct) ?? throw new InvalidOperationException("No se pudo recuperar la plantilla creada.");
    }

    private sealed class PlantillaFila
    {
        public long IdPlantilla { get; set; }
        public string NombreP { get; set; } = "";
        public decimal Presupuesto { get; set; }
        public int CantFutbolista { get; set; }
        public long IdUsuario { get; set; }
    }

    private sealed class DetalleFila
    {
        public long IdPlantilla { get; set; }
        public long IdFutbolista { get; set; }
        public bool FutbolistaTitular { get; set; }
    }
}