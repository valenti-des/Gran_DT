namespace GranDT.Dominio;

public sealed record Futbolista(
    long IdFutbolista,
    string Nombre,
    string Apellido,
    string? Apodo,
    DateOnly? FechaNac,
    decimal Cotizacion,
    long IdEquipo,
    TipoFutbolista Tipo,
    IReadOnlyList<Puntuacion> Puntuaciones)
{
    public const decimal CotizacionMaxima = 99_999_999.99m;

    public static void ValidarDatos(string nombre, string apellido, decimal cotizacion, long idEquipo, TipoFutbolista tipo)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            throw new ReglaNegocioException("Nombre y apellido son obligatorios.");
        if (cotizacion is < 0m or > CotizacionMaxima)
            throw new ReglaNegocioException("La cotización debe estar entre 0 y 99.999.999,99.");
        if (idEquipo <= 0 || !Enum.IsDefined(tipo))
            throw new ReglaNegocioException("El equipo y el tipo de futbolista deben ser válidos.");
    }

    public decimal PuntajeEnFecha(int nroFecha) =>
        Puntuaciones.FirstOrDefault(p => p.NroFecha == nroFecha)?.Valor ?? 0m;
}