namespace Biblio.IRepo;

public interface IRepoPlantilla
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorId(uint id);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(uint id);
}