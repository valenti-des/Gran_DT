using GranDT.Aplicacion;
using GranDT.Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT.Api.Controllers;

[ApiController, Route("api/usuarios")]
public sealed class UsuarioController(UsuarioServicio usuarios, PlantillaServicio plantillas) : ControllerBase
{
    /// <summary>Crea un usuario; la respuesta nunca incluye su contraseña.</summary>
    [HttpPost, ProducesResponseType(typeof(UsuarioRespuesta), StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<UsuarioRespuesta>> Crear([FromBody] CrearUsuarioHttp solicitud, CancellationToken ct)
    {
        var creado = (await usuarios.CrearAsync(solicitud.AAplicacion(), ct)).AHttp();
        return StatusCode(StatusCodes.Status201Created, creado);
    }

    /// <summary>Lista las plantillas de un usuario.</summary>
    [HttpGet("{id:long}/plantillas"), ProducesResponseType(typeof(IReadOnlyList<PlantillaRespuesta>), StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<PlantillaRespuesta>>> ListarPlantillas(long id, CancellationToken ct) =>
        Ok((await plantillas.ListarDeUsuarioAsync(id, ct)).Select(p => p.AHttp()).ToArray());

    /// <summary>Crea una plantilla para un usuario.</summary>
    [HttpPost("{id:long}/plantillas"), ProducesResponseType(typeof(PlantillaRespuesta), StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PlantillaRespuesta>> CrearPlantilla(long id, [FromBody] CrearPlantillaHttp solicitud, CancellationToken ct)
    {
        var creada = (await plantillas.CrearAsync(id, solicitud.AAplicacion(), ct)).AHttp();
        return StatusCode(StatusCodes.Status201Created, creada);
    }
}