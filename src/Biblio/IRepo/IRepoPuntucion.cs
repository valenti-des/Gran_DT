namespace Biblio.IRepo
{
    public interface  IRepoPuntucion
    {
        int Obtenerpuntucion(Puntuacion puntuacion);
        Puntuacion? ObtenerPuntuacionPorId(int idPuntuacion);
    }
}