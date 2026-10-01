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

    public List<PlantillaFutbolista> detalles { get; set; } = new List<PlantillaFutbolista>();

    public float PuntajeFecha(short cantFech)
    {
        if (!detalles.Any())
            return 0f;

        return detalles
            .Where(d => d.futbolistaTitular && d.futbolista != null)
            .Sum(d => d.futbolista!.ObtenerPuntuacionPorFecha(cantFech));
    }
}