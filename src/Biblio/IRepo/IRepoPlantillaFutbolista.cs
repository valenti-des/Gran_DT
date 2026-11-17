namespace Biblio.IRepo;

public interface IRepoPlantillaFutbolista
{
    List<PlantillaFutbolista> ObtenerTodos();
    PlantillaFutbolista? ObtenerPorId(uint idPlantilla, ushort idJugador);
    PlantillaFutbolista Agregar(PlantillaFutbolista plantillaFutbolista);
    bool Eliminar(uint idPlantilla, ushort idJugador);
}