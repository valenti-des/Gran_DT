namespace Biblio.IRepo;

public interface IRepoPlantilla
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorNombre(string nombre);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(string nombre);
}