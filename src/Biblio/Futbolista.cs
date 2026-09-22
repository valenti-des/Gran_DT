using System;
namespace Biblio;

public class Futbolista
{
    public ushort idFutbolista { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string? apodo { get; set; }
    public DateTime? fechaNac { get; set; }
    public decimal? cotizacion { get; set; }
    public byte? idEquipo { get; set; }
    public byte? idTipoFutbolista { get; set; }
    public List<Puntuacion>? puntuaciones { get; set; }
}
