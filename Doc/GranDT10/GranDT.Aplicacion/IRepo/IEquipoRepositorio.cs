using GranDT.Dominio;

namespace GranDT.Aplicacion.IRepo;

public interface IEquipoRepositorio
{
    Task<IReadOnlyList<Equipo>> ListarAsync(CancellationToken cancellationToken = default);
    Task<bool> ExisteAsync(long id, CancellationToken cancellationToken = default);
}