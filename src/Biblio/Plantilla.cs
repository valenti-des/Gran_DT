using System.Collections.Generic;
using System.Linq;

namespace Biblio;

public class Plantilla
{
    public uint idPlantilla { get; set; }
    public string nombreP { get; set; } = string.Empty;
    public decimal presupuesto { get; set; } = 90000000m;
    public byte cantFutbolista { get; set; }
    public ushort idUsuario { get; set; }


    public List<PlantillaFutbolista> detalles { get; set; } = new List<PlantillaFutbolista>();

    public decimal PuntajeFecha(byte numFech) =>
        detalles
            .Where(detalle => detalle.futbolistaTitular && detalle.futbolista is not null)
            .Sum(detalle => detalle.futbolista!.ObtenerPuntuacionPorFecha(numFech));
}

public class PlantillaCreacion
{
    public string nombreP { get; set; } = string.Empty;
    public decimal presupuesto { get; set; } = 90000000m;
    public byte cantFutbolista { get; set; }
    public ushort idUsuario { get; set; }
    public List<PlantillaFutbolista> detalles { get; set; } = new List<PlantillaFutbolista>();

}