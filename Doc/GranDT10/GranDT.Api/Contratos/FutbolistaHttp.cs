using System.ComponentModel.DataAnnotations;
using GranDT.Aplicacion;

namespace GranDT.Api;

public sealed record CrearFutbolistaHttp
{
    [Required] public string Nombre { get; init; } = "";
    [Required] public string Apellido { get; init; } = "";
    public string? Apodo { get; init; }
    public DateOnly? FechaNac { get; init; }
    [Range(typeof(decimal), "0", "99999999.99")] public decimal Cotizacion { get; init; }
    [Range(1, 255)] public long IdEquipo { get; init; }
    [Range(1, 4)] public byte IdTipoFutbolista { get; init; }
    public IReadOnlyList<PuntuacionHttp>? Puntuaciones { get; init; }

    public CrearFutbolista AAplicacion() => new(Nombre, Apellido, Apodo, FechaNac, Cotizacion,
        IdEquipo, (GranDT.Dominio.TipoFutbolista)IdTipoFutbolista,
        Puntuaciones?.Select(p => new NuevaPuntuacion(p.Puntuacion, p.NroFecha)).ToArray());
}

public sealed record ActualizarFutbolistaHttp
{
    [Required] public string Nombre { get; init; } = "";
    [Required] public string Apellido { get; init; } = "";
    public string? Apodo { get; init; }
    public DateOnly? FechaNac { get; init; }
    [Range(typeof(decimal), "0", "99999999.99")] public decimal Cotizacion { get; init; }
    [Range(1, 255)] public long IdEquipo { get; init; }
    [Range(1, 4)] public byte IdTipoFutbolista { get; init; }

    public ActualizarFutbolista AAplicacion() => new(Nombre, Apellido, Apodo, FechaNac,
        Cotizacion, IdEquipo, (GranDT.Dominio.TipoFutbolista)IdTipoFutbolista);
}

public sealed record FutbolistaRespuesta(long IdFutbolista, string Nombre, string Apellido, string? Apodo,
    DateOnly? FechaNac, decimal Cotizacion, long IdEquipo, int IdTipoFutbolista,
    IReadOnlyList<PuntuacionRespuesta> Puntuaciones);