using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPlantillaFutbolista : RepoDapper, IRepoPlantillaFutbolista
{
    private const string SelectSql = @"
        SELECT idFutbolistaPlantilla, futbolistaTitular, validacionP, validacionR,
               idPlantilla, idFutbolista
        FROM Futbolista_Plantilla";

    public RepoPlantillaFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    public List<PlantillaFutbolista> ObtenerTodos() =>
        Conexion.Query<PlantillaFutbolista>(SelectSql).AsList();

    public PlantillaFutbolista? ObtenerPorId(byte idPlantilla, ushort idJugador) =>
        Conexion.QuerySingleOrDefault<PlantillaFutbolista>(
            $"{SelectSql} WHERE idPlantilla = @IdPlantilla AND idFutbolista = @IdFutbolista LIMIT 1",
            new { IdPlantilla = idPlantilla, IdFutbolista = idJugador });

    public PlantillaFutbolista Agregar(PlantillaFutbolista plantillaFutbolista)
    {
        ArgumentNullException.ThrowIfNull(plantillaFutbolista);

        var parametros = new DynamicParameters();
        parametros.Add("unidFutbolistaPlantilla", dbType: DbType.Byte, direction: ParameterDirection.Output);
        parametros.Add("unfutbolistaTitular", plantillaFutbolista.futbolistaTitular);
        parametros.Add("unvalidacionP", plantillaFutbolista.validacionP);
        parametros.Add("unvalidacionR", plantillaFutbolista.validacionR);
        parametros.Add("unidPlantilla", plantillaFutbolista.idPlantilla);
        parametros.Add("unidFutbolista", plantillaFutbolista.idFutbolista);

        Conexion.Execute("AltaFutbolistaP", parametros, commandType: CommandType.StoredProcedure);
        plantillaFutbolista.idFutbolistaPlantilla = parametros.Get<byte>("unidFutbolistaPlantilla");

        return plantillaFutbolista;
    }

    public bool Eliminar(byte idPlantilla, ushort idJugador) =>
        Conexion.Execute(
            "DELETE FROM Futbolista_Plantilla WHERE idPlantilla = @IdPlantilla AND idFutbolista = @IdFutbolista",
            new { IdPlantilla = idPlantilla, IdFutbolista = idJugador }) > 0;
}