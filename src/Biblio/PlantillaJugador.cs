namespace Biblio;

public class PlantillaJugador
{
    public int idplantilla { get; set; } // FK -> Plantilla
    public int idjugador { get; set; }   // FK -> Jugador
    public bool Titular { get; set; }  // true = titular, false = suplente
}
