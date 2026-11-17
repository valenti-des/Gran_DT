using Microsoft.AspNetCore.Mvc;
using Biblio;
using Servicios;

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly ServiciosEquipo _serviciosEquipo;

    public EquipoController(ServiciosEquipo serviciosEquipo)
    {
        _serviciosEquipo = serviciosEquipo;
    }

    [HttpPost]
    public ActionResult<Equipo> Agregar([FromBody] EquipoCreacion equipo)
    {
        try
        {
            var nuevoEquipo = new Equipo
            {
                nombre = equipo.nombre
            };

            var creado = _serviciosEquipo.Agregar(nuevoEquipo);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idEquipo }, creado);
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

    [HttpGet]
    public ActionResult<IEnumerable<Equipo>> ObtenerTodos()
    {
        try
        {
            return Ok(_serviciosEquipo.ObtenerTodos());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Equipo> ObtenerPorId(byte idEquipo)
    {
        try
        {
            var equipo = _serviciosEquipo.ObtenerPorId(idEquipo);
            if (equipo == null)
            {
                return NotFound(new { error = "El equipo no existe." });
            }

            return Ok(equipo);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(byte idEquipo)
    {
        try
        {
            var equipo = _serviciosEquipo.ObtenerPorId(idEquipo);
            if (equipo is null)
                return NotFound(new { error = "El equipo no existe." });

            var eliminado = _serviciosEquipo.Eliminar(idEquipo);
            if (!eliminado)
                return StatusCode(500, new { error = "No se pudo eliminar el equipo." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
