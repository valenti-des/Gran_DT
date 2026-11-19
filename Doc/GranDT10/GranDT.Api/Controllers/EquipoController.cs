using GranDT.Aplicacion;
using GranDT.Aplicacion.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace GranDT.Api.Controllers;

[ApiController, Route("api/equipos")]
public sealed class EquipoController(EquipoServicio servicio) : ControllerBase
{
    /// <summary>Lista todos los equipos.</summary>
    [HttpGet, ProducesResponseType(typeof(IReadOnlyList<EquipoRespuesta>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<EquipoRespuesta>>> Listar(CancellationToken ct) =>
        Ok((await servicio.ListarAsync(ct)).Select(e => e.AHttp()).ToArray());
}