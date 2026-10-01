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

    public PlantillaFutbolista Agregar(PlantillaFutbolista plantillaJugador)
    {
        ArgumentNullException.ThrowIfNull(plantillaJugador);

        var parametros = new DynamicParameters();
        parametros.Add("unidFutbolistaPlantilla", dbType: DbType.Byte, direction: ParameterDirection.Output);
        parametros.Add("unfutbolistaTitular", plantillaJugador.futbolistaTitular);
        parametros.Add("unvalidacionP", plantillaJugador.validacionP);
        parametros.Add("unvalidacionR", plantillaJugador.validacionR);
        parametros.Add("unidPlantilla", plantillaJugador.idPlantilla);
        parametros.Add("unidFutbolista", plantillaJugador.idFutbolista);

        Conexion.Execute("AltaFutbolistaP", parametros, commandType: CommandType.StoredProcedure);
        plantillaJugador.idFutbolistaPlantilla = parametros.Get<byte>("unidFutbolistaPlantilla");

        return plantillaJugador;
    }

    public bool Eliminar(byte idPlantilla, ushort idJugador) =>
        Conexion.Execute(
            "DELETE FROM Futbolista_Plantilla WHERE idPlantilla = @IdPlantilla AND idFutbolista = @IdFutbolista",
            new { IdPlantilla = idPlantilla, IdFutbolista = idJugador }) > 0;
}