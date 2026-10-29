using System.Data;
using Biblio;
using Biblio.IRepo;
using Dapper;

namespace BiblioConDapper;

public class RepoTipoFutbolista : RepoDapper, IRepoTipoFutbolista
{
    public RepoTipoFutbolista(IDbConnection conexion) : base(conexion)
    {
    }

    public List<TipoFutbolista> ObtenerTodos() =>
        Conexion.Query<TipoFutbolista>("SELECT idTipoFutbolista, tipoFutbolista FROM Tipo_Futbolista").AsList();

    public TipoFutbolista? ObtenerPorId(byte idTipoFutbolista) =>
        Conexion.QuerySingleOrDefault<TipoFutbolista>(
            "SELECT idTipoFutbolista, tipoFutbolista FROM Tipo_Futbolista WHERE idTipoFutbolista = @Id LIMIT 1",
            new { Id = idTipoFutbolista });


    public TipoFutbolista Agregar(TipoFutbolista tipoFutbolista)
    {
        ArgumentNullException.ThrowIfNull(tipoFutbolista);
        tipoFutbolista.tipoFutbolista = tipoFutbolista.tipoFutbolista.Trim();
        var parametros = new DynamicParameters();
        parametros.Add("unidTipoFutbolista", dbType: DbType.Byte, direction: ParameterDirection.Output);
        parametros.Add("untipoFutbolista", tipoFutbolista.tipoFutbolista);

        Conexion.Execute("AltaTipoFutbolista", parametros, commandType: CommandType.StoredProcedure);
        tipoFutbolista.idTipoFutbolista = parametros.Get<byte>("unidTipoFutbolista");

        return tipoFutbolista;
    }

    public bool Eliminar(byte idTipoFutbolista)
    {
        return Conexion.Execute("DELETE FROM Tipo_Futbolista WHERE idTipoFutbolista = @Id", new { Id = idTipoFutbolista }) > 0;
    }
}