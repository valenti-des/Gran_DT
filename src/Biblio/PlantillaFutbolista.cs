namespace Biblio;

using System.ComponentModel;
using System.Text.Json.Serialization;

public class PlantillaFutbolista
{
    [JsonIgnore]
    public uint idPlantilla { get; set; }
    [JsonIgnore]
    public ushort idFutbolista { get; set; }
    public bool futbolistaTitular { get; set; }
    [ReadOnly(true)]
    public string nombreFutbolista { get; set; } = string.Empty;

    // Propiedad de navegación vital para calcular el puntaje
    [JsonIgnore]
    public Futbolista? futbolista { get; set; }
}

public class PlantillaFutbolistaCreacion
{
    public uint idPlantilla { get; set; }
    public ushort idFutbolista { get; set; }
    public bool futbolistaTitular { get; set; }
}
