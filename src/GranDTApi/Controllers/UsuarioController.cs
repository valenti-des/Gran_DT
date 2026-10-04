using System.ComponentModel.DataAnnotations;
using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>Representación pública de un usuario en la API.</summary>
public sealed record UsuarioResponse(
    short IdUsuario,
    string Nombre,
    string Apellido,
    string Email,
    DateOnly? FechaNacimiento,
    bool EsAdmin);

/// <summary>Datos necesarios para crear un usuario.</summary>
public sealed record CrearUsuarioRequest
{
    [Required]
    public required string Nombre { get; init; }

    [Required]
    public required string Apellido { get; init; }

    [Required, EmailAddress]
    public required string Email { get; init; }

    public DateOnly? FechaNacimiento { get; init; }

    [Required, MinLength(8)]
    public required string Contraseña { get; init; }
}

/// <summary>Endpoints para gestionar usuarios.</summary>
[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ServiciosUsuario _usuarioService;

    public UsuarioController(ServiciosUsuario usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var usuarios = _usuarioService.ObtenerTodos().Select(Mapear);
        return Ok(usuarios);
    }

    [HttpGet("{nombre}")]
    public IActionResult ObtenerPorNombre(string nombre)
    {
        var usuario = _usuarioService.ObtenerPorNombre(nombre);

        if (usuario is null)
        {
            return NotFound();
        }

        return Ok(Mapear(usuario));
    }

    [HttpDelete("{email}")]
    public IActionResult Eliminar(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El email del usuario es obligatorio."
            });
        }

        var eliminado = _usuarioService.Eliminar(email);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearUsuarioRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre) ||
            string.IsNullOrWhiteSpace(request.Apellido) ||
            string.IsNullOrWhiteSpace(request.Email) ||
            string.IsNullOrWhiteSpace(request.Contraseña))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Nombre, apellido, email y contraseña son obligatorios."
            });
        }

        var usuario = _usuarioService.Agregar(new Usuario
        {
            nombre = request.Nombre,
            apellido = request.Apellido,
            email = request.Email,
            fechaNac = request.FechaNacimiento,
            contraseña = request.Contraseña
        });

        var respuesta = Mapear(usuario);
        return CreatedAtAction(nameof(ObtenerPorNombre), new { nombre = usuario.nombre }, respuesta);
    }

    private static UsuarioResponse Mapear(Usuario usuario) =>
        new(usuario.idUsuario, usuario.nombre, usuario.apellido, usuario.email, usuario.fechaNac, usuario.es_admin);
}
