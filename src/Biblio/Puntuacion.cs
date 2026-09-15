namespace Biblio;

public class Puntuacion
{
    public int idpuntuacion { get; set; }
    public int idjugador { get; set; } // FK -> Jugador
    public int fecha_numero { get; set; }  // número de fecha (< 50)
    public decimal nota { get; set; }  // del 1 al 10
}
