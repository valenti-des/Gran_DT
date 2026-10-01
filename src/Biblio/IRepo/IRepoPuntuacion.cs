namespace Biblio.IRepo;

public interface IRepoPuntuacion
{
    List<Puntuacion> ObtenerTodos();
    Puntuacion? ObtenerPorId(uint idPuntuacion);
    Puntuacion Agregar(Puntuacion puntuacion);
    bool Eliminar(uint idPuntuacion);
}