using GranDT.Dominio;

namespace GranDT.Aplicacion.IRepo;

public interface IPlantillaRepositorio
{
    Task<Plantilla?> ObtenerAsync(long id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Plantilla>> ListarDeUsuarioAsync(long idUsuario, CancellationToken cancellationToken = default);
    Task<Plantilla> CrearAsync(string nombre, decimal presupuesto, int cantidad, long idUsuario, CancellationToken cancellationToken = default);
}