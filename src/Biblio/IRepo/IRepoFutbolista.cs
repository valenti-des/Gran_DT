namespace Biblio.IRepo;

public interface IRepoFutbolista
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(ushort id);
    Futbolista Agregar(Futbolista jugador);
    bool Eliminar(ushort id);
}