using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Aplicacion.Servicios;

public sealed class UsuarioServicio(IUsuarioRepositorio usuarios, IHashContrasenas hasher)
{
    public async Task<Usuario> CrearAsync(CrearUsuario solicitud, CancellationToken ct = default)
    {
        ReglasUsuario.Validar(solicitud.Nombre, solicitud.Apellido, solicitud.Email, solicitud.Contrasena);
        var email = solicitud.Email.Trim().ToLowerInvariant();
        if (await usuarios.ExisteEmailAsync(email, ct)) throw new ConflictoException("El correo ya está registrado.");
        var hash = hasher.Crear(solicitud.Contrasena);
        return await usuarios.CrearAsync(solicitud.Nombre.Trim(), solicitud.Apellido.Trim(), email,
            solicitud.FechaNac, hash, solicitud.EsAdmin, ct);
    }
}