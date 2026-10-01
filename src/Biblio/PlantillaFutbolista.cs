namespace Biblio;

public class PlantillaFutbolista
{
    public byte idFutbolistaPlantilla { get; set; }
    public bool futbolistaTitular { get; set; } 
    // futbolistaSuplente ELIMINADO: Si es false, ya sabemos que es suplente.
    public bool validacionP { get; set; }
    public bool validacionR { get; set; }
    public byte idPlantilla { get; set; }
    public ushort idFutbolista { get; set; }

    // Propiedad de navegación vital para calcular el puntaje
    public Futbolista? futbolista { get; set; }
}