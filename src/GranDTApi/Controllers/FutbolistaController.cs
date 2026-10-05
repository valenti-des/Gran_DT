using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>
/// Representación pública de un futbolista en la API.
/// </summary>
public sealed record FutbolistaResponse(
    ushort IdFutbolista,
    string Nombre,
    string Apellido,
    string? Apodo,
    DateOnly? FechaNacimiento,
    decimal? Cotizacion,
    byte? IdEquipo,
    byte? IdTipoFutbolista,
    IReadOnlyList<PuntuacionResponse> Puntuaciones);

/// <summary>
/// Representación de una puntuación en la API.
/// </summary>
public sealed record PuntuacionResponse(uint IdPuntuacion, float? Puntuacion, DateOnly? Fecha);

/// <summary>Endpoints para gestionar futbolistas.</summary>
[ApiController]
[Route("api/[controller]")]
public class FutbolistaController(ServiciosFutbolista futbolistaService) : ControllerBase
{
    /// <summary>
    /// Obtiene todos los futbolistas.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<FutbolistaResponse>> ObtenerTodos()
    {
        var futbolistas = futbolistaService.ObtenerTodos().Select(Mapear);
        return Ok(futbolistas);
    }

    /// <summary>
    /// Obtiene un futbolista por su nombre.
    /// </summary>
    [HttpGet("{nombre}")]
    public ActionResult<FutbolistaResponse> ObtenerPorNombre(string nombre)
    {
        var futbolista = futbolistaService.ObtenerPorNombre(nombre);

        if (futbolista is null)
        {
            return NotFound();
        }

        return Ok(Mapear(futbolista));
    }

    /// <summary>
    /// Elimina un futbolista por su nombre.
    /// </summary>
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

        var eliminado = futbolistaService.Eliminar(nombre);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Crea un nuevo futbolista.
    /// </summary>
    [HttpPost]
    public ActionResult<FutbolistaResponse> Crear([FromBody] Futbolista request)
    {
        if (string.IsNullOrWhiteSpace(request.nombre) || string.IsNullOrWhiteSpace(request.apellido))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre y apellido del futbolista son obligatorios."
            });
        }

        var futbolista = futbolistaService.Agregar(new Futbolista
        {
            nombre = request.nombre,
            apellido = request.apellido,
            apodo = request.apodo,
            fechaNac = request.fechaNac,
            cotizacion = request.cotizacion,
            idEquipo = request.idEquipo,
            idTipoFutbolista = request.idTipoFutbolista
        });

        return CreatedAtAction(
            nameof(ObtenerPorNombre),
            new { nombre = futbolista.nombre },
            Mapear(futbolista));
    }

    /// <summary>
    /// Convierte un Futbolista en FutbolistaResponse.
    /// </summary>
    private static FutbolistaResponse Mapear(Futbolista futbolista)
    {
        return new FutbolistaResponse(
            futbolista.idFutbolista,
            futbolista.nombre,
            futbolista.apellido,
            futbolista.apodo,
            futbolista.fechaNac,
            futbolista.cotizacion,
            futbolista.idEquipo,
            futbolista.idTipoFutbolista,
            futbolista.puntuaciones
                .Select(puntuacion => new PuntuacionResponse(
                    puntuacion.idPuntuacion,
                    puntuacion.puntuacion,
                    puntuacion.cantFech))
                .ToList());
    }
}
