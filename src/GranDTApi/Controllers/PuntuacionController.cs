using Microsoft.AspNetCore.Mvc;
using Biblio;
using Servicios;

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
    private readonly ServiciosPuntuacion _serviciosPuntuacion;

    public PuntuacionController(ServiciosPuntuacion serviciosPuntuacion)
    {
        _serviciosPuntuacion = serviciosPuntuacion;
    }

    [HttpPost]
    public ActionResult<Puntuacion> Agregar([FromBody] PuntuacionCreacion puntuacion)
    {
        try
        {
            var nuevaPuntuacion = new Puntuacion
            {
                idFutbolista = puntuacion.idFutbolista,
                puntuacion = puntuacion.puntuacion,
                nroFecha = puntuacion.nroFecha
            };

            var creado = _serviciosPuntuacion.Agregar(nuevaPuntuacion);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idPuntuacion }, creado);
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
    public ActionResult<IEnumerable<Puntuacion>> ObtenerTodos()
    {
        try
        {
            return Ok(_serviciosPuntuacion.ObtenerTodos());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Puntuacion> ObtenerPorId(byte idPuntuacion)
    {
        try
        {
            var puntuacion = _serviciosPuntuacion.ObtenerPorId(idPuntuacion);
            if (puntuacion == null)
            {
                return NotFound(new { error = "La puntuación no existe." });
            }

            return Ok(puntuacion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(uint idPuntuacion)
    {
        try
        {
            var puntuacion = _serviciosPuntuacion.ObtenerPorId(idPuntuacion);
            if (puntuacion is null)
                return NotFound(new { error = "La puntuación no existe." });

            var eliminado = _serviciosPuntuacion.Eliminar(idPuntuacion);
            if (!eliminado)
                return StatusCode(500, new { error = "No se pudo eliminar la puntuación." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
