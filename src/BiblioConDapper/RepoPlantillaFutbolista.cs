using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPlantillaFutbolista : RepoDapper, IRepoPlantillaFutbolista
{
    private const string SelectSql = @"
        SELECT futbolistaTitular, idPlantilla, idFutbolista
        FROM Futbolista_Plantilla";

    public RepoPlantillaFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    public List<PlantillaFutbolista> ObtenerTodos() =>
        Conexion.Query<PlantillaFutbolista>(SelectSql).AsList();

    public List<PlantillaFutbolista> ObtenerPorPlantilla(uint idPlantilla) =>
        Conexion.Query<PlantillaFutbolista>(
            $"{SelectSql} WHERE idPlantilla = @IdPlantilla ORDER BY idFutbolista",
            new { IdPlantilla = idPlantilla }).AsList();

    public PlantillaFutbolista? ObtenerPorId(uint idPlantilla, ushort idFutbolista) =>
        Conexion.QuerySingleOrDefault<PlantillaFutbolista>(
            $"{SelectSql} WHERE idPlantilla = @IdPlantilla AND idFutbolista = @IdFutbolista LIMIT 1",
            new { IdPlantilla = idPlantilla, IdFutbolista = idFutbolista });

    public PlantillaFutbolista Agregar(PlantillaFutbolista plantillaFutbolista)
    {
        Conexion.Execute("AgregarFutbolistaAPlantilla", new
        {
            unidPlantilla = plantillaFutbolista.idPlantilla,
            unidFutbolista = plantillaFutbolista.idFutbolista,
            unfutbolistaTitular = plantillaFutbolista.futbolistaTitular
        }, commandType: CommandType.StoredProcedure);

        return plantillaFutbolista;
    }

    public bool Eliminar(uint idPlantilla, ushort idFutbolista) =>
        Conexion.Execute(
            "DELETE FROM Futbolista_Plantilla WHERE idPlantilla = @IdPlantilla AND idFutbolista = @IdFutbolista",
            new { IdPlantilla = idPlantilla, IdFutbolista = idFutbolista }) > 0;
}