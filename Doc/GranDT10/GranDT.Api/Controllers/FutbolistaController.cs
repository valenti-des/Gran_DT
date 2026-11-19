using GranDT.Aplicacion;
using GranDT.Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT.Api.Controllers;

[ApiController, Route("api/futbolistas")]
public sealed class FutbolistaController(FutbolistaServicio servicio) : ControllerBase
{
    /// <summary>Lista futbolistas.</summary>
    [HttpGet, ProducesResponseType(typeof(IReadOnlyList<FutbolistaRespuesta>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<FutbolistaRespuesta>>> Listar(CancellationToken ct) =>
        Ok((await servicio.ListarAsync(ct)).Select(f => f.AHttp()).ToArray());

    /// <summary>Consulta un futbolista por identificador.</summary>
    [HttpGet("{id:long}"), ProducesResponseType(typeof(FutbolistaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FutbolistaRespuesta>> Obtener(long id, CancellationToken ct) =>
        (await servicio.ObtenerAsync(id, ct)).AHttp();

    /// <summary>Crea un futbolista y, opcionalmente, registra puntuaciones iniciales.</summary>
    [HttpPost, ProducesResponseType(typeof(FutbolistaRespuesta), StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<FutbolistaRespuesta>> Crear([FromBody] CrearFutbolistaHttp solicitud, CancellationToken ct)
    {
        var creado = (await servicio.CrearAsync(solicitud.AAplicacion(), ct)).AHttp();
        return CreatedAtAction(nameof(Obtener), new { id = creado.IdFutbolista }, creado);
    }

    /// <summary>Actualiza los datos y la cotización de un futbolista.</summary>
    [HttpPut("{id:long}"), ProducesResponseType(typeof(FutbolistaRespuesta), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FutbolistaRespuesta>> Actualizar(long id, [FromBody] ActualizarFutbolistaHttp solicitud, CancellationToken ct) =>
        (await servicio.ActualizarAsync(id, solicitud.AAplicacion(), ct)).AHttp();
}