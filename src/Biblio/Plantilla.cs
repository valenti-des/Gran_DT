namespace Biblio;

public class Plantilla
{
    public int idplantilla { get; set; }
    public int idusuario { get; set; } // FK -> Usuario
    public int cantidad_jugador {get; set;}
    public decimal presupuesto { get; set; }
    
}
