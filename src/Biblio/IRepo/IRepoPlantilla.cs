namespace Biblio.IRepo;

public interface IRepoPlantilla
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorId(byte idPlantilla);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(byte idPlantilla);
}