using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoEquipo : RepoDapper, IEquipoRepository
{
    public RepoEquipo(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Equipo> ObtenerTodos() =>
        Conexion.Query<Equipo>("SELECT idEquipo, nombre FROM Equipo").AsList();

    public Equipo? ObtenerPorNombre(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return null;

        return Conexion.QuerySingleOrDefault<Equipo>(
            "SELECT idEquipo, nombre FROM Equipo WHERE nombre = @Nombre LIMIT 1",
            new { Nombre = nombre.Trim() });
    }

    public Equipo Agregar(Equipo equipo)
    {
        ArgumentNullException.ThrowIfNull(equipo);

        if (string.IsNullOrWhiteSpace(equipo.nombre))
            throw new ArgumentException("El nombre es obligatorio.", nameof(equipo));

        equipo.nombre = equipo.nombre.Trim();
        equipo.idEquipo = Conexion.QuerySingle<byte>(@"
            INSERT INTO Equipo (nombre)
            VALUES (@Nombre);
            SELECT LAST_INSERT_ID();", new { Nombre = equipo.nombre });

        return equipo;
    }

    public bool Eliminar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            return false;

        return Conexion.Execute("DELETE FROM Equipo WHERE nombre = @Nombre", new { Nombre = nombre.Trim() }) > 0;
    }
}