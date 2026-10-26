namespace Biblio.IRepo;

public interface IRepoFutbolista
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(string id);
    Futbolista Agregar(Futbolista futbolista);
    bool Eliminar(string id);
}