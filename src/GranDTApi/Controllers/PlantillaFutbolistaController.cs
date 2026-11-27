using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;
using System.Security.Claims;

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/Plantilla/{idPlantilla}")]
public class PlantillaFutbolistaController : ControllerBase
{
    private readonly ServiciosPlantillaFutbolista _serviciosPlantillaFutbolista;

    public PlantillaFutbolistaController(ServiciosPlantillaFutbolista serviciosPlantillaFutbolista)
    {
        _serviciosPlantillaFutbolista = serviciosPlantillaFutbolista;
    }

    [HttpGet("futbolistas")]
    public ActionResult<IEnumerable<PlantillaFutbolista>> ObtenerPorPlantilla(uint idPlantilla)
    {
        try
        {
            return Ok(_serviciosPlantillaFutbolista.ObtenerPorPlantilla(idPlantilla));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("futbolistas/{idFutbolista}")]
    public ActionResult<PlantillaFutbolista> ObtenerPorId(uint idPlantilla, ushort idFutbolista)
    {
        try
        {
            var asociacion = _serviciosPlantillaFutbolista.ObtenerPorId(idPlantilla, idFutbolista);
            if (asociacion is null)
                return NotFound(new { error = "El futbolista no está asociado a esta plantilla." });

            return Ok(asociacion);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost("~/api/Plantilla/futbolistas")]
    public ActionResult<PlantillaFutbolista> Agregar([FromBody] PlantillaFutbolistaCreacion solicitud)
    {
        try
        {
            if (!TryObtenerIdUsuario(out var idUsuario))
                return Unauthorized();

            var asociacion = _serviciosPlantillaFutbolista.Agregar(
                solicitud.idPlantilla,
                idUsuario,
                solicitud.idFutbolista,
                solicitud.futbolistaTitular);
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { idPlantilla = asociacion.idPlantilla, idFutbolista = asociacion.idFutbolista },
                asociacion);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            return NotFound(new { error = "La plantilla no existe." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("futbolistas/{idFutbolista}")]
    public ActionResult Eliminar(uint idPlantilla, ushort idFutbolista)
    {
        try
        {
            if (!TryObtenerIdUsuario(out var idUsuario))
                return Unauthorized();

            if (!_serviciosPlantillaFutbolista.Eliminar(idPlantilla, idUsuario, idFutbolista))
                return NotFound(new { error = "El futbolista no está asociado a esta plantilla." });

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            return NotFound(new { error = "La plantilla no existe." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    private bool TryObtenerIdUsuario(out ushort idUsuario) =>
        ushort.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out idUsuario);
}
