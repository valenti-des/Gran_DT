namespace Biblio;

public class PlantillaFutbolista
{
    public uint idPlantilla { get; set; }
    public ushort idFutbolista { get; set; }
    public bool futbolistaTitular { get; set; }

    // Propiedad de navegación vital para calcular el puntaje
    public Futbolista? futbolista { get; set; }
}

public class PlantillaFutbolistaCreacion
{
    public uint idPlantilla { get; set; }
    public ushort idFutbolista { get; set; }
    public bool futbolistaTitular { get; set; }
    public Futbolista? futbolista { get; set; }
}

