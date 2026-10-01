namespace Biblio.IRepo;

public interface IRepoPlantillaFutbolista
{
    List<PlantillaFutbolista> ObtenerTodos();
    PlantillaFutbolista? ObtenerPorId(byte idPlantilla, ushort idJugador);
    PlantillaFutbolista Agregar(PlantillaFutbolista plantillaJugador);
    bool Eliminar(byte idPlantilla, ushort idJugador);
}