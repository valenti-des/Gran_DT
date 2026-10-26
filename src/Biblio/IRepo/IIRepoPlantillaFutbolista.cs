namespace Biblio.IRepo;

public interface IRepoPlantillaFutbolista
{
    List<PlantillaFutbolista> ObtenerTodos();
    PlantillaFutbolista? ObtenerPorId(byte idPlantilla, ushort idJugador);
    PlantillaFutbolista Agregar(PlantillaFutbolista plantillaFutbolista);
    bool Eliminar(byte idPlantilla, ushort idJugador);
}