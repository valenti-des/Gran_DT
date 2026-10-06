using System.ComponentModel.DataAnnotations;
using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>Datos necesarios para crear una plantilla.</summary>
public sealed record CrearPlantillaRequest
{
    [Required]
    public required string Nombre { get; init; }

    public decimal? CantidadMaximaMonto { get; init; }
    public byte? CantidadMaximaFutbolistas { get; init; }
    public short? IdUsuario { get; init; }
}

/// <summary>Endpoints para gestionar plantillas.</summary>
[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly ServiciosPlantilla _plantillaService;

    public PlantillaController(ServiciosPlantilla plantillaService)
    {
        _plantillaService = plantillaService;
    }

    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var plantillas = _plantillaService.ObtenerTodos();
        return Ok(plantillas);
    }

    [HttpGet("{nombre}")]
    public IActionResult ObtenerPorNombre(string nombre)
    {
        var plantilla = _plantillaService.ObtenerPorNombre(nombre);

        if (plantilla is null)
        {
            return NotFound();
        }

        return Ok(plantilla);
    }

    [HttpDelete("{nombre}")]
    public IActionResult Eliminar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre de la plantilla es obligatorio."
            });
        }

        var eliminada = _plantillaService.Eliminar(nombre);

        if (!eliminada)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost]
    public IActionResult Crear([FromBody] CrearPlantillaRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre de la plantilla es obligatorio."
            });
        }

        var plantilla = _plantillaService.Agregar(new Plantilla
        {
            nombreP = request.Nombre,
            cantMaxMonto = request.CantidadMaximaMonto,
            cantMaxFutbolista = request.CantidadMaximaFutbolistas,
            idUsuario = request.IdUsuario
        });

        return CreatedAtAction(
            nameof(ObtenerPorNombre),
            new { nombre = plantilla.nombreP },
            plantilla);
    }
}
