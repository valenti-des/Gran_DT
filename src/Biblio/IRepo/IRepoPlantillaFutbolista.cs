namespace Biblio.IRepo;

public interface IRepoPlantillaFutbolista
{
    List<PlantillaFutbolista> ObtenerTodos();
    List<PlantillaFutbolista> ObtenerPorPlantilla(uint idPlantilla);
    PlantillaFutbolista? ObtenerPorId(uint idPlantilla, ushort idFutbolista);
    PlantillaFutbolista Agregar(PlantillaFutbolista plantillaFutbolista);
    bool Eliminar(uint idPlantilla, ushort idFutbolista);
}