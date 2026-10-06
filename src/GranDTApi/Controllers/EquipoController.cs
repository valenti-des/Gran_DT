using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblio;
using Servicios;

namespace GranDTApi.Controllers;

public sealed record CrearEquipoRequest(
    [Required(ErrorMessage = "El nombre del equipo es obligatorio.")] string Nombre);

/// <summary>Representación de un equipo en la API.</summary>
public sealed record EquipoResponse(byte IdEquipo, string Nombre);

[ApiController]
[Route("api/[controller]")]
public class EquipoController(ServiceEquipo equipoService) : ControllerBase
{
    /// <summary>
    /// Obtiene todos los equipos.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<EquipoResponse>> ObtenerTodos()
    {
        var equipos = equipoService.ObtenerTodos().Select(Mapear);
        return Ok(equipos);
    }

    /// <summary>
    /// Obtiene un equipo por su nombre.
    /// </summary>
    [HttpGet("{nombre}")]
    public ActionResult<EquipoResponse> ObtenerPorNombre(string nombre)
    {
        var equipo = equipoService.ObtenerPorNombre(nombre);

        if (equipo == null)
        {
            return NotFound();
        }

        return Ok(Mapear(equipo));
    }

    /// <summary>
    /// Elimina un equipo por su nombre.
    /// </summary>
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

        var eliminado = equipoService.Eliminar(nombre);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Crea un nuevo equipo.
    /// </summary>
    [HttpPost]
    public ActionResult<EquipoResponse> Crear([FromBody] CrearEquipoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Nombre))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre del equipo es obligatorio."
            });
        }

        var equipo = equipoService.Agregar(new Equipo { nombre = request.Nombre });

        var respuesta = Mapear(equipo);
        return CreatedAtAction(nameof(ObtenerPorNombre), new { nombre = equipo.nombre }, respuesta);
    }

    /// <summary>
    /// Convierte un Equipo en EquipoResponse.
    /// </summary>
    private static EquipoResponse Mapear(Equipo equipo) => new(equipo.idEquipo, equipo.nombre);
}
