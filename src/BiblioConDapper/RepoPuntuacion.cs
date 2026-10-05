  using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPuntuacion : RepoDapper, IRepoPuntuacion
{
    private const string SelectSql = @"
        SELECT idPuntuacion, idFutbolista, puntuacion, cantFech
        FROM Puntuacion";

    public RepoPuntuacion(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Puntuacion> ObtenerTodos() => Conexion.Query<Puntuacion>(SelectSql).AsList();

    public Puntuacion? ObtenerPorId(uint idPuntuacion) =>
        Conexion.QuerySingleOrDefault<Puntuacion>(
            $"{SelectSql} WHERE idPuntuacion = @IdPuntuacion LIMIT 1",
            new { IdPuntuacion = idPuntuacion });

    public Puntuacion Agregar(Puntuacion puntuacion)
    {
        ArgumentNullException.ThrowIfNull(puntuacion);

        var parametros = new DynamicParameters();
        parametros.Add("unidPuntuacion", dbType: DbType.UInt32, direction: ParameterDirection.Output);
        parametros.Add("unidFutbolista", puntuacion.idFutbolista);
        parametros.Add("unpuntuacion", puntuacion.puntuacion);
        parametros.Add("uncantFech", puntuacion.cantFech);

        Conexion.Execute("AltaPuntuacion", parametros, commandType: CommandType.StoredProcedure);
        puntuacion.idPuntuacion = parametros.Get<uint>("unidPuntuacion");

        return puntuacion;
    }

    public bool Eliminar(uint idPuntuacion) =>
        Conexion.Execute("DELETE FROM Puntuacion WHERE idPuntuacion = @IdPuntuacion", new { IdPuntuacion = idPuntuacion }) > 0;
}