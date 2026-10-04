using System.ComponentModel.DataAnnotations;
using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>Datos necesarios para crear un futbolista.</summary>
public sealed record CrearFutbolistaRequest
{
    [Required]
    public required string Nombre { get; init; }

    [Required]
    public required string Apellido { get; init; }

    public string? Apodo { get; init; }
    public DateOnly? FechaNacimiento { get; init; }
    public decimal? Cotizacion { get; init; }
    public byte? IdEquipo { get; init; }
    public byte? IdTipoFutbolista { get; init; }
}

/// <summary>Endpoints para gestionar futbolistas.</summary>
[ApiController]
[Route("api/[controller]")]
public class FutbolistaController : ControllerBase
{
    private readonly ServiciosFutbolista _futbolistaService;

    public FutbolistaController(ServiciosFutbolista futbolistaService)
    {
        _futbolistaService = futbolistaService;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var futbolistas = _futbolistaService.ObtenerTodos();
        return Ok(futbolistas);
    }

    [HttpGet("{nombre}")]
    public IActionResult ObtenerPorNombre(string nombre)
    {
        var futbolista = _futbolistaService.ObtenerPorNombre(nombre);

        if (futbolista is null)
        {
            return NotFound();
        }

        return Ok(futbolista);
    }

    [HttpDelete("{nombre}")]
    public IActionResult Eliminar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre del futbolista es obligatorio."
            });
        }

        var eliminado = _futbolistaService.Eliminar(nombre);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearFutbolistaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre) || string.IsNullOrWhiteSpace(request.Apellido))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre y apellido del futbolista son obligatorios."
            });
        }

        var futbolista = _futbolistaService.Agregar(new Futbolista
        {
            nombre = request.Nombre,
            apellido = request.Apellido,
            apodo = request.Apodo,
            fechaNac = request.FechaNacimiento,
            cotizacion = request.Cotizacion,
            idEquipo = request.IdEquipo,
            idTipoFutbolista = request.IdTipoFutbolista
        });

        return CreatedAtAction(
            nameof(ObtenerPorNombre),
            new { nombre = futbolista.nombre },
            futbolista);
    }
}
