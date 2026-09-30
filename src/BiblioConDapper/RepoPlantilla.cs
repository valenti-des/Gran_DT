using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPlantilla : RepoDapper, IPlantillaRepository
{
    private const string SelectSql = @"
        SELECT idPlantilla, nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario, idPuntuacion
        FROM Plantilla";

    public RepoPlantilla(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Plantilla> ObtenerTodos() => Conexion.Query<Plantilla>(SelectSql).AsList();

    public Plantilla? ObtenerPorId(byte idPlantilla) =>
        Conexion.QuerySingleOrDefault<Plantilla>(
            $"{SelectSql} WHERE idPlantilla = @IdPlantilla LIMIT 1",
            new { IdPlantilla = idPlantilla });

    public Plantilla Agregar(Plantilla plantilla)
    {
        ArgumentNullException.ThrowIfNull(plantilla);

        if (string.IsNullOrWhiteSpace(plantilla.nombreP))
            throw new ArgumentException("El nombre de la plantilla es obligatorio.", nameof(plantilla));

        plantilla.nombreP = plantilla.nombreP.Trim();
        plantilla.idPlantilla = Conexion.QuerySingle<byte>(@"
            INSERT INTO Plantilla (nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario, idPuntuacion)
            VALUES (@NombreP, @CantMaxMonto, @CantMaxFutbolista, @IdUsuario, @IdPuntuacion);
            SELECT LAST_INSERT_ID();", new
        {
            NombreP = plantilla.nombreP,
            CantMaxMonto = plantilla.cantMaxMonto,
            CantMaxFutbolista = plantilla.cantMaxFutbolista,
            IdUsuario = plantilla.idUsuario,
            IdPuntuacion = plantilla.idPuntuacion
        });

        return plantilla;
    }

    public bool Eliminar(byte idPlantilla) =>
        Conexion.Execute("DELETE FROM Plantilla WHERE idPlantilla = @IdPlantilla", new { IdPlantilla = idPlantilla }) > 0;
}