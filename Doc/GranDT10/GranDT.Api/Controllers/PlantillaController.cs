using System.ComponentModel.DataAnnotations;
using GranDT.Aplicacion;
using GranDT.Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT.Api.Controllers;

[ApiController, Route("api/plantillas")]
public sealed class PlantillaController(PlantillaServicio plantillas, PlantillaFutbolistaServicio integrantes) : ControllerBase
{
    /// <summary>Consulta una plantilla con sus futbolistas.</summary>
    [HttpGet("{id:long}"), ProducesResponseType(typeof(PlantillaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlantillaRespuesta>> Obtener(long id, CancellationToken ct) =>
        (await plantillas.ObtenerAsync(id, ct)).AHttp();

    /// <summary>Incorpora un futbolista si no está duplicado y respeta presupuesto y cantidad máxima.</summary>
    [HttpPost("{id:long}/jugadores"), ProducesResponseType(typeof(PlantillaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PlantillaRespuesta>> Agregar(long id, [FromBody] AgregarJugadorHttp solicitud, CancellationToken ct)
    {
        if (solicitud.FutbolistaTitular is null) return BadRequest(new { error = "futbolistaTitular es obligatorio." });
        return (await integrantes.AgregarAsync(id, solicitud.IdFutbolista, solicitud.FutbolistaTitular.Value, ct)).AHttp();
    }

    /// <summary>Cambia la condición de un integrante entre titular y suplente.</summary>
    [HttpPut("{id:long}/jugadores/{futbolistaId:long}"), ProducesResponseType(typeof(PlantillaFutbolistaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlantillaFutbolistaRespuesta>> CambiarCondicion(long id, long futbolistaId,
        [FromBody] CambiarCondicionHttp solicitud, CancellationToken ct)
    {
        if (solicitud.FutbolistaTitular is null) return BadRequest(new { error = "futbolistaTitular es obligatorio." });
        var detalle = await integrantes.CambiarCondicionAsync(id, futbolistaId, solicitud.FutbolistaTitular.Value, ct);
        return new PlantillaFutbolistaRespuesta(detalle.IdPlantilla, detalle.Futbolista.IdFutbolista, detalle.FutbolistaTitular);
    }

    /// <summary>Devuelve errores de validación de presupuesto, cantidad y formación titular.</summary>
    [HttpGet("{id:long}/validacion"), ProducesResponseType(typeof(ValidacionPlantillaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ValidacionPlantillaRespuesta>> Validar(long id, CancellationToken ct)
    {
        var plantilla = await plantillas.ObtenerAsync(id, ct);
        var errores = plantilla.Validar();
        return new ValidacionPlantillaRespuesta(errores.Count == 0, plantilla.CostoActual, errores);
    }

    /// <summary>Calcula el puntaje de los titulares; una jornada sin puntuación aporta cero.</summary>
    [HttpGet("{id:long}/puntaje"), ProducesResponseType(typeof(PuntajePlantillaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PuntajePlantillaRespuesta>> Puntaje(long id, [FromQuery, Range(1, 49)] int nroFecha, CancellationToken ct) =>
        new PuntajePlantillaRespuesta(id, nroFecha, await plantillas.PuntajeAsync(id, nroFecha, ct));
}