using GranDT.Aplicacion;
using GranDT.Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT.Api.Controllers;

[ApiController, Route("api/futbolistas/{id:long}/puntuaciones")]
public sealed class PuntuacionController(PuntuacionServicio servicio) : ControllerBase
{
    /// <summary>Registra la puntuación de un futbolista en una jornada.</summary>
    [HttpPost, ProducesResponseType(typeof(PuntuacionRespuesta), StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<PuntuacionRespuesta>> Registrar(long id, [FromBody] RegistrarPuntuacionHttp solicitud, CancellationToken ct)
    {
        var creada = (await servicio.CrearAsync(id, solicitud.Puntuacion, solicitud.NroFecha, ct)).AHttp();
        return StatusCode(StatusCodes.Status201Created, creada);
    }
}