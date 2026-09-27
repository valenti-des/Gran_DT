using Biblio;

namespace Biblio.IRepo
{
    public interface IRepoTipoFutbolista
    {
        IEnumerable<TipoFutbolista> obtenerTiposFutbolistas();
        Task<IEnumerable<TipoFutbolista>> ObtenerTiposFutbolistasAsync(CancellationToken cancellationToken);
    }
}