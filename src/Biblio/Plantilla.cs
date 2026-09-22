namespace Biblio;

public class Plantilla
{
    public byte idPlantilla { get; set; }
    public string nombreP { get; set; } = string.Empty;
    public decimal? cantMaxMonto { get; set; }
    public byte? cantMaxFutbolista { get; set; }
    public short? idUsuario { get; set; }
    public uint? idPuntuacion { get; set; }
}
