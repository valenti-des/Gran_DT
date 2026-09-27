namespace GranDTApi.Servicios;

public interface ITipoFutbolistaServicio
{
    Task<IReadOnlyList<TipoFutbolistaResponse>> ObtenerTodosAsync(CancellationToken cancellationToken);
}

/// <summary>Representa un tipo de futbolista devuelto por la API.</summary>
public sealed record TipoFutbolistaResponse(byte IdTipoFutbolista, string TipoFutbolista);