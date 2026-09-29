namespace Biblio.IRepo;

public interface IPlantillaRepository
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorId(byte idPlantilla);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(byte idPlantilla);
}