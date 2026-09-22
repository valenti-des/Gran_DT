using System.Data;

namespace BiblioConDapper;

public abstract class RepoDapper
{
    protected readonly IDbConnection Conexion;

    public RepoDapper(IDbConnection conexion) => Conexion = conexion;
}