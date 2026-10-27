namespace Biblio;

public class Puntuacion
{
    public uint idPuntuacion { get; set; }
    public ushort idFutbolista { get; set; }
    public decimal? puntuacion { get; set; }
    public byte? numFech { get; set; }
}

public class PuntuacionCreacion
{
    public ushort idFutbolista { get; set; }
    public decimal? puntuacion { get; set; }
    public byte? numFech { get; set; }
}