namespace Biblio.IRepo;

public interface IRepoPlantilla
{
    List<Plantilla> ObtenerTodos();
    Plantilla? ObtenerPorId(ushort id);
    Plantilla Agregar(Plantilla plantilla);
    bool Eliminar(ushort id);
}