namespace GranDT.Dominio;

public sealed record Puntuacion(long IdPuntuacion, long IdFutbolista, decimal Valor, byte NroFecha)
{
    public static void Validar(decimal valor, int nroFecha)
    {
        if (valor is < 1m or > 10m) throw new ReglaNegocioException("La puntuación debe estar entre 1 y 10.");
        if (nroFecha is < 1 or > 49) throw new ReglaNegocioException("La fecha debe estar entre 1 y 49.");
    }
}