using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoEquipo : RepoDapper, IRepoEquipo
{
    public RepoEquipo(IDbConnection conexion) : base(conexion)
    {
    }

    public List<Equipo> ObtenerTodos() =>
        Conexion.Query<Equipo>("SELECT idEquipo, nombre FROM Equipo").AsList();

    public Equipo? ObtenerPorId(byte idEquipo) =>
        Conexion.QuerySingleOrDefault<Equipo>(
            "SELECT idEquipo, nombre FROM Equipo WHERE idEquipo = @Id LIMIT 1",
            new { Id = idEquipo });


    public Equipo Agregar(Equipo equipo)
    {
        ArgumentNullException.ThrowIfNull(equipo);
        equipo.nombre = equipo.nombre.Trim();
        var parametros = new DynamicParameters();
        parametros.Add("unidEquipo", dbType: DbType.Byte, direction: ParameterDirection.Output);
        parametros.Add("unnombre", equipo.nombre);

        Conexion.Execute("AltaEquipo", parametros, commandType: CommandType.StoredProcedure);
        equipo.idEquipo = parametros.Get<byte>("unidEquipo");

        return equipo;
    }

    public bool Eliminar(byte idEquipo)
    {
        return Conexion.Execute("DELETE FROM Equipo WHERE idEquipo = @Id", new { Id = idEquipo }) > 0;
    }
}