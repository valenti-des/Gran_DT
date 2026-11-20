using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Biblio;

public class Plantilla
{
    public uint idPlantilla { get; set; }
    public string nombreP { get; set; } = string.Empty;
    [ReadOnly(true)]
    public decimal presupuesto { get; set; } = 90000000m;
    [ReadOnly(true)]
    public byte cantFutbolista { get; set; }
    [ReadOnly(true)]
    public byte maxFutbolistas { get; } = 20;
    public ushort idUsuario { get; set; }


    public List<PlantillaFutbolista> detalles { get; set; } = new List<PlantillaFutbolista>();

    public decimal PuntajeFecha(byte nroFecha) =>
        detalles
            .Where(detalle => detalle.futbolistaTitular && detalle.futbolista is not null)
            .Sum(detalle => detalle.futbolista!.ObtenerPuntuacionPorFecha(nroFecha));
}

public class PlantillaCreacion
{
    public string nombreP { get; set; } = string.Empty;
    public ushort idUsuario { get; set; }
}