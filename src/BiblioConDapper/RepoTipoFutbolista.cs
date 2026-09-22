using System.Data;
using Biblio.IRepo;
using Biblio;
using Dapper;

namespace BiblioConDapper
{
    public class RepoTipoFutbolista : RepoDapper, IRepoTipoFutbolista
    {
        public RepoTipoFutbolista(IDbConnection conexion)
        : base(conexion){}
        private const string Sql = "SELECT * FROM Tipo_Futbolista";
        public IEnumerable<TipoFutbolista> obtenerTiposFutbolistas()
        {
            return Conexion.Query<TipoFutbolista>(Sql);
        }
    }
    
    
    
}