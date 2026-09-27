using Biblio.IRepo;

namespace GranDTApi.Servicios;

public sealed class TipoFutbolistaServicio(IRepoTipoFutbolista repositorio) : ITipoFutbolistaServicio
{
    public async Task<IReadOnlyList<TipoFutbolistaResponse>> ObtenerTodosAsync(
        CancellationToken cancellationToken)
    {
        var tipos = await repositorio.ObtenerTiposFutbolistasAsync(cancellationToken);
        return tipos
            .Select(tipo => new TipoFutbolistaResponse(tipo.idTipoFutbolista, tipo.tipoFutbolista))
            .ToArray();
    }
}