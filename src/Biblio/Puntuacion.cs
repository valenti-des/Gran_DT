namespace Biblio;

public class Puntuacion
{
    public uint idPuntuacion { get; set; }
    public ushort idFutbolista { get; set; }
    public float? puntuacion { get; set; }
    public DateOnly? cantFech { get; set; }
}

public class PuntuacionCreacion
{
    public ushort idFutbolista { get; set; }
    public float? puntuacion { get; set; }
    public DateOnly? cantFech { get; set; }
}