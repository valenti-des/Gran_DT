using System.Data;
namespace Dapper;

public abstract class RepoDapper
{
    protected readonly IDbConnection _conexion;

    public RepoDapper(IDbConnection conexion) => _conexion = conexion;
}