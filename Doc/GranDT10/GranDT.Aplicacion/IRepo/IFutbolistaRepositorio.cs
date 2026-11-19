using GranDT.Dominio;

namespace GranDT.Aplicacion.IRepo;

public interface IFutbolistaRepositorio
{
    Task<IReadOnlyList<Futbolista>> ListarAsync(CancellationToken cancellationToken = default);
    Task<Futbolista?> ObtenerAsync(long id, CancellationToken cancellationToken = default);
    Task<Futbolista> CrearAsync(Futbolista futbolista, IReadOnlyCollection<(decimal Valor, byte NroFecha)> puntuaciones, CancellationToken cancellationToken = default);
    Task<Futbolista?> ActualizarAsync(Futbolista futbolista, CancellationToken cancellationToken = default);
}