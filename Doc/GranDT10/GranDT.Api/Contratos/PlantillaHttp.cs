using System.ComponentModel.DataAnnotations;
using GranDT.Aplicacion;

namespace GranDT.Api;

public sealed record CrearPlantillaHttp
{
    [Required] public string NombreP { get; init; } = "";
    [Range(typeof(decimal), "0", "100000000")] public decimal Presupuesto { get; init; }
    [Range(1, 20)] public int CantFutbolista { get; init; }
    public CrearPlantilla AAplicacion() => new(NombreP, Presupuesto, CantFutbolista);
}

public sealed record AgregarJugadorHttp([Range(1, long.MaxValue)] long IdFutbolista, bool? FutbolistaTitular);
public sealed record CambiarCondicionHttp(bool? FutbolistaTitular);
public sealed record PlantillaFutbolistaRespuesta(long IdPlantilla, long IdFutbolista, bool FutbolistaTitular);
public sealed record PlantillaRespuesta(long IdPlantilla, string NombreP, decimal Presupuesto, int CantFutbolista,
    long IdUsuario, IReadOnlyList<PlantillaFutbolistaRespuesta> Detalles);
public sealed record ValidacionPlantillaRespuesta(bool Valida, decimal CostoActual, IReadOnlyList<string> Errores);
public sealed record PuntajePlantillaRespuesta(long IdPlantilla, int NroFecha, decimal Puntaje);