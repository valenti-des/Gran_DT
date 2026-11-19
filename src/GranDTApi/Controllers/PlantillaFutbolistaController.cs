using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

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

    [HttpPost("futbolistas")]
    [HttpPost("idfutbolistas")]
    public ActionResult<PlantillaFutbolista> Agregar(
        uint idPlantilla,
        [FromBody] PlantillaFutbolistaCreacion solicitud)
    {
        try
        {
            var asociacion = _serviciosPlantillaFutbolista.Agregar(
                idPlantilla,
                solicitud.idFutbolista,
                solicitud.futbolistaTitular);
            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { idPlantilla, idFutbolista = asociacion.idFutbolista },
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
            if (!_serviciosPlantillaFutbolista.Eliminar(idPlantilla, idFutbolista))
                return NotFound(new { error = "El futbolista no está asociado a esta plantilla." });

            return NoContent();
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
}
