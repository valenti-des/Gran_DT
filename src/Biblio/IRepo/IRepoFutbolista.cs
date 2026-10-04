namespace Biblio.IRepo;

public interface IRepoFutbolista
{
    List<Futbolista> ObtenerTodos();
    Futbolista? ObtenerPorNombre(string nombre);
    Futbolista Agregar(Futbolista jugador);
    bool Eliminar(string nombre);
}