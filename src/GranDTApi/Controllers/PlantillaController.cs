using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>
/// Representación pública de una plantilla en la API.
/// </summary>
public sealed record PlantillaResponse(
    byte IdPlantilla,
    string Nombre,
    decimal? CantidadMaximaMonto,
    byte? CantidadMaximaFutbolistas,
    short? IdUsuario,
    IReadOnlyList<PlantillaFutbolistaResponse> Detalles);

/// <summary>
/// Representación de un futbolista incluido en una plantilla.
/// </summary>
public sealed record PlantillaFutbolistaResponse(
    byte IdFutbolistaPlantilla,
    bool EsTitular,
    bool ValidacionP,
    bool ValidacionR,
    byte IdPlantilla,
    ushort IdFutbolista,
    FutbolistaResponse? Futbolista);

/// <summary>Endpoints para gestionar plantillas.</summary>
[ApiController]
[Route("api/[controller]")]
public class PlantillaController(ServiciosPlantilla plantillaService) : ControllerBase
{
    /// <summary>
    /// Obtiene todas las plantillas.
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<PlantillaResponse>> ObtenerTodos()
    {
        var plantillas = plantillaService.ObtenerTodos().Select(Mapear);
        return Ok(plantillas);
    }

    /// <summary>
    /// Obtiene una plantilla por su nombre.
    /// </summary>
    [HttpGet("{nombre}")]
    public ActionResult<PlantillaResponse> ObtenerPorNombre(string nombre)
    {
        var plantilla = plantillaService.ObtenerPorNombre(nombre);

        if (plantilla is null)
        {
            return NotFound();
        }

        return Ok(Mapear(plantilla));
    }

    /// <summary>
    /// Elimina una plantilla por su nombre.
    /// </summary>
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

        var eliminada = plantillaService.Eliminar(nombre);

        if (!eliminada)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Crea una nueva plantilla.
    /// </summary>
    [HttpPost]
    public ActionResult<PlantillaResponse> Crear([FromBody] Plantilla request)
    {
        if (string.IsNullOrWhiteSpace(request.nombreP))
        {
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "El nombre de la plantilla es obligatorio."
            });
        }

        var plantilla = plantillaService.Agregar(new Plantilla
        {
            nombreP = request.nombreP,
            cantMaxMonto = request.cantMaxMonto,
            cantMaxFutbolista = request.cantMaxFutbolista,
            idUsuario = request.idUsuario
        });

        return CreatedAtAction(
            nameof(ObtenerPorNombre),
            new { nombre = plantilla.nombreP },
            Mapear(plantilla));
    }

    /// <summary>
    /// Convierte una Plantilla en PlantillaResponse.
    /// </summary>
    private static PlantillaResponse Mapear(Plantilla plantilla)
    {
        return new PlantillaResponse(
            plantilla.idPlantilla,
            plantilla.nombreP,
            plantilla.cantMaxMonto,
            plantilla.cantMaxFutbolista,
            plantilla.idUsuario,
            plantilla.detalles
                .Select(detalle => new PlantillaFutbolistaResponse(
                    detalle.idFutbolistaPlantilla,
                    detalle.futbolistaTitular,
                    detalle.validacionP,
                    detalle.validacionR,
                    detalle.idPlantilla,
                    detalle.idFutbolista,
                    detalle.futbolista is null ? null : FutbolistaControllerMapear(detalle.futbolista)))
                .ToList());
    }

    private static FutbolistaResponse FutbolistaControllerMapear(Futbolista futbolista)
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
