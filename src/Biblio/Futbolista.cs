using System;
using System.Collections.Generic;
using System.Linq;

namespace Biblio;

public class Futbolista
{
    public ushort idFutbolista { get; set; }
    public string nombre { get; set; } = string.Empty;//messi es piola
    public string apellido { get; set; } = string.Empty;
    public string? apodo { get; set; }
    public DateOnly fechaNac { get; set; } 
    public decimal? cotizacion { get; set; }
    public byte? idEquipo { get; set; }
    public byte? idTipoFutbolista { get; set; }
    
    // Lista inicializada para evitar NullReferenceExceptions
    public List<Puntuacion> puntuaciones { get; set; } = new List<Puntuacion>();

    // Método que busca la puntuación del jugador para una fecha específica
    public decimal ObtenerPuntuacionPorFecha(byte numFech)
    {
        return puntuaciones.FirstOrDefault(p => p.numFech == numFech)?.puntuacion ?? 0m;
    }
}

public class FutbolistaCreacion
{
    public string nombre { get; set; } = string.Empty;//messi es piola
    
    public string apellido { get; set; } = string.Empty;
    public string? apodo { get; set; }
    public DateOnly fechaNac { get; set; } 
    public decimal? cotizacion { get; set; }
    public byte? idEquipo { get; set; }
    public byte idTipoFutbolista { get; set; }
    public List<Puntuacion>? puntuaciones { get; set; } = new List<Puntuacion>();
}