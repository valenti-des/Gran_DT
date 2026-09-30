using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoFutbolista : RepoDapper, IJugadorRepository
{
    private const string SelectSql = @"
        SELECT idFutbolista, nombre, apellido, apodo, fechaNac, cotizacion,
               idEquipo, idTipoFutbolista
        FROM Futbolista";

    public RepoFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Futbolista> ObtenerTodos() => Conexion.Query<Futbolista>(SelectSql).AsList();

    public Futbolista? ObtenerPorId(ushort id) =>
        Conexion.QuerySingleOrDefault<Futbolista>(
            $"{SelectSql} WHERE idFutbolista = @IdFutbolista LIMIT 1",
            new { IdFutbolista = id });

    public Futbolista Agregar(Futbolista jugador)
    {
        ArgumentNullException.ThrowIfNull(jugador);

        if (string.IsNullOrWhiteSpace(jugador.nombre))
            throw new ArgumentException("El nombre es obligatorio.", nameof(jugador));

        if (string.IsNullOrWhiteSpace(jugador.apellido))
            throw new ArgumentException("El apellido es obligatorio.", nameof(jugador));

        jugador.nombre = jugador.nombre.Trim();
        jugador.apellido = jugador.apellido.Trim();
        jugador.apodo = jugador.apodo?.Trim();
        jugador.idFutbolista = Conexion.QuerySingle<ushort>(@"
            INSERT INTO Futbolista (nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista)
            VALUES (@Nombre, @Apellido, @Apodo, @FechaNac, @Cotizacion, @IdEquipo, @IdTipoFutbolista);
            SELECT LAST_INSERT_ID();", new
        {
            Nombre = jugador.nombre,
            Apellido = jugador.apellido,
            Apodo = jugador.apodo,
            FechaNac = jugador.fechaNac,
            Cotizacion = jugador.cotizacion,
            IdEquipo = jugador.idEquipo,
            IdTipoFutbolista = jugador.idTipoFutbolista
        });

        return jugador;
    }

    public bool Eliminar(ushort id) =>
        Conexion.Execute("DELETE FROM Futbolista WHERE idFutbolista = @IdFutbolista", new { IdFutbolista = id }) > 0;
}