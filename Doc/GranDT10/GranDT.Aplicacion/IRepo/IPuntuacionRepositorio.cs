using GranDT.Dominio;

namespace GranDT.Aplicacion.IRepo;

public interface IPuntuacionRepositorio
{
    Task<Puntuacion> CrearAsync(long idFutbolista, decimal valor, byte nroFecha, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Puntuacion>> ListarDeFutbolistaAsync(long idFutbolista, CancellationToken cancellationToken = default);
    Task<bool> ExisteEnFechaAsync(long idFutbolista, byte nroFecha, CancellationToken cancellationToken = default);
}