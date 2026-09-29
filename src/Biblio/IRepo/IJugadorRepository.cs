namespace Biblio.IRepo;

public interface IJugadorRepository
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorId(ushort id);
    Futbolista Agregar(Futbolista jugador);
    bool Eliminar(ushort id);
}