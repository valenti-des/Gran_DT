namespace Biblio.IRepo;

public interface IRepoTipoFutbolista
{
    List<TipoFutbolista> ObtenerTodos();
    TipoFutbolista? ObtenerPorId(byte idTipoFutbolista);
    TipoFutbolista Agregar(TipoFutbolista tipoFutbolista);
    bool Eliminar(byte idTipoFutbolista);
}
