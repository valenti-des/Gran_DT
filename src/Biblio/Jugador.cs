namespace Biblio;

public class Jugador
{
    public int idjugador { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string? apodo { get; set; }
    public DateTime fecha_nacimiento { get; set; }
    public decimal cotizacion { get; set; }
    public int idequipo { get; set; }
    public int idtipo { get; set; } // FK -> TipoJugador
    public List<Puntuacion>? puntuaciones { get; set; }
}
