namespace Biblio.IRepo;

public interface IPlantillaJugadorRepository
{
    List<PlantillaFutbolista> ObtenerTodos();
    PlantillaFutbolista? ObtenerPorId(byte idPlantilla, ushort idJugador);
    PlantillaFutbolista Agregar(PlantillaFutbolista plantillaJugador);
    bool Eliminar(byte idPlantilla, ushort idJugador);
}