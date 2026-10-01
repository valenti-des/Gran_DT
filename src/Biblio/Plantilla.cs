using System.Collections.Generic;
using System.Linq;

namespace Biblio;

public class Plantilla
{
    public byte idPlantilla { get; set; }
    public string nombreP { get; set; } = string.Empty;
    public decimal? cantMaxMonto { get; set; }
    public byte? cantMaxFutbolista { get; set; }
    public short? idUsuario { get; set; }
    
    // Lista de futbolistas asociados a esta plantilla
    public List<PlantillaFutbolista> detalles { get; set; } = new List<PlantillaFutbolista>();

    // Método dinámico: calcula la suma del puntaje de TODOS los integrantes en la fecha indicada
    public float PuntajeFecha(short cantFech)
    {
        if (detalles == null || !detalles.Any()) 
            return 0f;

        // Suma los puntos de todos los futbolistas de la plantilla en esa fecha
        return detalles
            .Where(d => d.futbolista != null)
            .Sum(d => d.futbolista!.ObtenerPuntuacionPorFecha(cantFech));
    }
}