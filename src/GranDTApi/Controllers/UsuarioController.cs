using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>
/// Controlador para gestionar los usuarios.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UsuarioController(ServiciosUsuario usuarioService) : ControllerBase
{
    /// <summary>
    /// Obtiene todos los usuarios.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<UsuarioResponse>> ObtenerTodos()
    {
        var usuarios = usuarioService
            .ObtenerTodos()
            .Select(Mapear);

        return Ok(usuarios);
    }

    /// <summary>
    /// Obtiene un usuario por su nombre.
    /// </summary>
    [HttpGet("{nombre}")]
    public ActionResult<UsuarioResponse> ObtenerPorNombre(string nombre)
    {
        var usuario = usuarioService.ObtenerPorNombre(nombre);

        if (usuario is null)
        {
            return NotFound();
        }

        return Ok(Mapear(usuario));
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    [HttpPost]
    public ActionResult<UsuarioResponse> Crear([FromBody] Usuario request)
    {
        var usuario = usuarioService.Agregar(new Usuario
        {
            nombre = request.nombre,
            apellido = request.apellido,
            email = request.email,
            fechaNac = request.fechaNac,
            contraseña = request.contraseña,
            es_admin = request.es_admin
        });

        var respuesta = Mapear(usuario);

        return CreatedAtAction(
            nameof(ObtenerPorNombre),
            new { nombre = usuario.nombre },
            respuesta);
    }

    /// <summary>
    /// Elimina un usuario por su email.
    /// </summary>
    [HttpDelete("{email}")]
    public IActionResult Eliminar(string email)
    {
        var eliminado = usuarioService.Eliminar(email);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Convierte un Usuario en UsuarioResponse.
    /// </summary>
    private static UsuarioResponse Mapear(Usuario usuario)
    {
        return new UsuarioResponse(
            usuario.idUsuario,
            usuario.nombre,
            usuario.apellido,
            usuario.email,
            usuario.fechaNac,
            usuario.es_admin);
    }
}

/// <summary>
/// Representación pública de un usuario en la API.
/// No incluye la contraseña.
/// </summary>
public sealed record UsuarioResponse(
    short IdUsuario,
    string Nombre,
    string Apellido,
    string Email,
    DateOnly? FechaNacimiento,
    bool EsAdmin);