using System.ComponentModel.DataAnnotations;

namespace GranDT.Api;

public sealed record PuntuacionHttp([Range(typeof(decimal), "1", "10")] decimal Puntuacion, [Range(1, 49)] int NroFecha);
public sealed record RegistrarPuntuacionHttp([Range(typeof(decimal), "1", "10")] decimal Puntuacion, [Range(1, 49)] int NroFecha);
public sealed record PuntuacionRespuesta(long IdPuntuacion, long IdFutbolista, decimal Puntuacion, int NroFecha);