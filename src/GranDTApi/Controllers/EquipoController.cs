using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblio;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>Representación de un equipo en la API.</summary>
public sealed record EquipoResponse(byte IdEquipo, string Nombre);

/// <summary>Datos necesarios para crear un equipo.</summary>
public sealed record CrearEquipoRequest
{
    [Required, StringLength(45)]
    public required string Nombre { get; init; }
}

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly IEquipoService _equipoService;

    public EquipoController(IEquipoService equipoService)
    {
        _equipoService = equipoService;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var equipos = _equipoService.ObtenerTodos().Select(Mapear);
        return Ok(equipos);
    }

    [HttpGet("{id:int}")]
    public IActionResult ObtenerPorId(byte id)
    {
        var equipo = _equipoService.ObtenerPorId(id);

        if (equipo == null)
        {
            return NotFound();
        }

        return Ok(Mapear(equipo));
    }

    [HttpDelete("{nombre}")]
    public IActionResult Eliminar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre del equipo es obligatorio."
            });
        }

        var eliminado = _equipoService.Eliminar(nombre);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearEquipoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre del equipo es obligatorio."
            });
        }

        var equipo = _equipoService.Crear(new Equipo { nombre = request.Nombre });

        if (equipo is null)
        {
            return Conflict(new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Ya existe un equipo con ese nombre."
            });
        }

        var respuesta = Mapear(equipo);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = equipo.idEquipo }, respuesta);
    }

    private static EquipoResponse Mapear(Equipo equipo) => new(equipo.idEquipo, equipo.nombre);
}

