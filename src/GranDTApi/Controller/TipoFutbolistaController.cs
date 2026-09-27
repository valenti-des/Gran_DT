using GranDTApi.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDTApi.Controller;

[ApiController]
[Route("api/tipos-futbolista")]
public sealed class TipoFutbolistaController(ITipoFutbolistaServicio servicio) : ControllerBase
{
    /// <summary>Obtiene los tipos de futbolista disponibles.</summary>
    [HttpGet]
    [EndpointSummary("Obtener tipos de futbolista")]
    [EndpointDescription("Devuelve los tipos de futbolista registrados en la base de datos.")]
    [Produces("application/json")]
    [ProducesResponseType<IReadOnlyList<TipoFutbolistaResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TipoFutbolistaResponse>>> ObtenerTodos(
        CancellationToken cancellationToken)
    {
        var tipos = await servicio.ObtenerTodosAsync(cancellationToken);
        return Ok(tipos);
    }
}