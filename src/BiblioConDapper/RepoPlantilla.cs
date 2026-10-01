using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPlantilla : RepoDapper, IPlantillaRepository
{
    private const string SelectSql = @"
        SELECT idPlantilla, nombreP, cantMaxMonto, cantMaxFutbolista, idUsuario
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
        var parametros = new DynamicParameters();
        parametros.Add("unidPlantilla", dbType: DbType.Byte, direction: ParameterDirection.Output);
        parametros.Add("unnombreP", plantilla.nombreP);
        parametros.Add("uncantMaxMonto", plantilla.cantMaxMonto);
        parametros.Add("uncantMaxFutbolista", plantilla.cantMaxFutbolista);
        parametros.Add("unidUsuario", plantilla.idUsuario);

        Conexion.Execute("AltaPlantilla", parametros, commandType: CommandType.StoredProcedure);
        plantilla.idPlantilla = parametros.Get<byte>("unidPlantilla");

        return plantilla;
    }

    public bool Eliminar(byte idPlantilla) =>
        Conexion.Execute("DELETE FROM Plantilla WHERE idPlantilla = @IdPlantilla", new { IdPlantilla = idPlantilla }) > 0;
}