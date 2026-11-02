namespace Biblio.IRepo;

public interface IRepoFutbolista
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(ushort id);
    Futbolista Agregar(Futbolista futbolista);
    bool Eliminar(ushort id);
}