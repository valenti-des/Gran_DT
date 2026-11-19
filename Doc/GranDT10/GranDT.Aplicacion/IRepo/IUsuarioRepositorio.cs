using GranDT.Dominio;

namespace GranDT.Aplicacion.IRepo;

public interface IUsuarioRepositorio
{
    Task<Usuario?> ObtenerAsync(long id, CancellationToken cancellationToken = default);
    Task<bool> ExisteEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Usuario> CrearAsync(string nombre, string apellido, string email, DateOnly? fechaNac, string hashContrasena, bool esAdmin, CancellationToken cancellationToken = default);
}