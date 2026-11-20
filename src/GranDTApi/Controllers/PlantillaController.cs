using Microsoft.AspNetCore.Mvc;
using Biblio;
using Servicios;

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly ServiciosPlantilla _serviciosPlantilla;

    public PlantillaController(ServiciosPlantilla serviciosPlantilla)
    {
        _serviciosPlantilla = serviciosPlantilla;
    }

    [HttpPost]
    public ActionResult<Plantilla> Agregar([FromBody] PlantillaCreacion plantilla)
    {
        try
        {
            var nuevoPlantilla = new Plantilla
            {
                nombreP = plantilla.nombreP,
                presupuesto = 90000000m,
                cantFutbolista = 0,
                idUsuario = plantilla.idUsuario
            };

            var creado = _serviciosPlantilla.Agregar(nuevoPlantilla);
            return CreatedAtAction(nameof(ObtenerPorId), new { idPlantilla = creado.idPlantilla }, creado);
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
    public ActionResult<IEnumerable<Plantilla>> ObtenerTodos()
    {
        try
        {
            return Ok(_serviciosPlantilla.ObtenerTodos());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{idPlantilla}")]
    public ActionResult<Plantilla> ObtenerPorId(uint idPlantilla)
    {
        try
        {
            var plantilla = _serviciosPlantilla.ObtenerPorId(idPlantilla);
            if (plantilla == null)
            {
                return NotFound(new { error = "La plantilla no existe." });
            }

            return Ok(plantilla);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{idPlantilla}")]
    public ActionResult Eliminar(uint idPlantilla)
    {
        try
        {
            var plantilla = _serviciosPlantilla.ObtenerPorId(idPlantilla);
            if (plantilla is null)
                return NotFound(new { error = "La plantilla no existe." });

            var eliminado = _serviciosPlantilla.Eliminar(idPlantilla);
            if (!eliminado)
                return StatusCode(500, new { error = "No se pudo eliminar la plantilla." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
