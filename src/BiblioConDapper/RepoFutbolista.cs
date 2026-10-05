using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoFutbolista : RepoDapper, IRepoFutbolista
{
    private const string SelectSql = @"
        SELECT idFutbolista, nombre, apellido, apodo, fechaNac, cotizacion,
               idEquipo, idTipoFutbolista
        FROM Futbolista";

    public RepoFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Futbolista> ObtenerTodos()
    {
        var futbolistas = Conexion.Query<Futbolista>(SelectSql).AsList();
        CargarPuntuaciones(futbolistas);
        return futbolistas;
    }

    public Futbolista? ObtenerPorId(ushort id)
    {
        var futbolista = Conexion.QuerySingleOrDefault<Futbolista>(
            $"{SelectSql} WHERE idFutbolista = @IdFutbolista LIMIT 1",
            new { IdFutbolista = id });

        if (futbolista is null)
            return null;

        futbolista.puntuaciones = Conexion.Query<Puntuacion>(
            "SELECT idPuntuacion, idFutbolista, puntuacion, cantFech FROM Puntuacion WHERE idFutbolista = @IdFutbolista",
            new { IdFutbolista = id }).AsList();

        return futbolista;
    }

    public Futbolista? ObtenerPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return null;

        var futbolista = Conexion.QuerySingleOrDefault<Futbolista>(
            $"{SelectSql} WHERE nombre = @Nombre LIMIT 1",
            new { Nombre = nombre.Trim() });

        if (futbolista is not null)
        {
            futbolista.puntuaciones = Conexion.Query<Puntuacion>(
                "SELECT idPuntuacion, idFutbolista, puntuacion, cantFech FROM Puntuacion WHERE idFutbolista = @IdFutbolista",
                new { IdFutbolista = futbolista.idFutbolista }).AsList();
        }

        return futbolista;
    }


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

        var sql = @"
            INSERT INTO Futbolista (nombre, apellido, apodo, fechaNac, cotizacion, idEquipo, idTipoFutbolista)
            VALUES (@Nombre, @Apellido, @Apodo, @FechaNac, @Cotizacion, @IdEquipo, @IdTipoFutbolista);
            SELECT LAST_INSERT_ID();";

        var idGenerado = Conexion.ExecuteScalar<ulong>(sql, new
        {
            Nombre = jugador.nombre,
            Apellido = jugador.apellido,
            Apodo = jugador.apodo,
            FechaNac = jugador.fechaNac,
            Cotizacion = jugador.cotizacion,
            IdEquipo = jugador.idEquipo,
            IdTipoFutbolista = jugador.idTipoFutbolista
        });

        jugador.idFutbolista = (ushort)idGenerado;
        return jugador;
    }

    public bool Eliminar(ushort id) =>
        Conexion.Execute("DELETE FROM Futbolista WHERE idFutbolista = @IdFutbolista", new { IdFutbolista = id }) > 0;

    public bool Eliminar(string nombre) =>
        !string.IsNullOrWhiteSpace(nombre) &&
        Conexion.Execute("DELETE FROM Futbolista WHERE nombre = @Nombre", new { Nombre = nombre.Trim() }) > 0;
}