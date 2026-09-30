using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoPuntuacion : RepoDapper, IPuntuacionRepository
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

        puntuacion.idPuntuacion = Conexion.QuerySingle<uint>(@"
            INSERT INTO Puntuacion (idFutbolista, puntuacion, cantFech)
            VALUES (@IdFutbolista, @Puntuacion, @CantFech);
            SELECT LAST_INSERT_ID();", new
        {
            puntuacion.idFutbolista,
            puntuacion.puntuacion,
            puntuacion.cantFech
        });

        return puntuacion;
    }

    public bool Eliminar(uint idPuntuacion) =>
        Conexion.Execute("DELETE FROM Puntuacion WHERE idPuntuacion = @IdPuntuacion", new { IdPuntuacion = idPuntuacion }) > 0;
}