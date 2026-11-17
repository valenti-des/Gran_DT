using Microsoft.AspNetCore.Mvc;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly ServiciosPlantilla _serviciosPlantilla;
    private readonly IRepoPlantillaFutbolista _plantillaFutbolistaRepository;

    public PlantillaController(
        ServiciosPlantilla serviciosPlantilla,
        IRepoPlantillaFutbolista plantillaFutbolistaRepository)
    {
        _serviciosPlantilla = serviciosPlantilla;
        _plantillaFutbolistaRepository = plantillaFutbolistaRepository;
    }

    [HttpPost]
    public ActionResult<Plantilla> Agregar([FromBody] PlantillaCreacion plantilla)
    {
        try
        {
            var nuevoPlantilla = new Plantilla
            {
                nombreP = plantilla.nombreP,
                presupuesto = plantilla.presupuesto,
                cantFutbolista = plantilla.cantFutbolista,
                idUsuario = plantilla.idUsuario
            };

            var creado = _serviciosPlantilla.Agregar(nuevoPlantilla);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idPlantilla }, creado);
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

    [HttpPost("{idPlantilla}/idfutbolistas")]
    public ActionResult<PlantillaFutbolista> AgregarFutbolista(
        uint idPlantilla,
        [FromBody] PlantillaFutbolistaCreacion solicitud)
    {
        try
        {
            var plantilla = _serviciosPlantilla.ObtenerPorId(idPlantilla);
            if (plantilla is null)
                return NotFound(new { error = "La plantilla no existe." });

            if (_plantillaFutbolistaRepository.ObtenerPorId(idPlantilla, solicitud.idFutbolista) is not null)
                return Conflict(new { error = "El futbolista ya está asociado a esta plantilla." });

            const byte cantidadMaxima = 20;
            if (plantilla.detalles.Count >= cantidadMaxima)
                return Conflict(new { error = "La plantilla alcanzó su cantidad máxima de futbolistas." });

            var asociacion = new PlantillaFutbolista
            {
                idPlantilla = idPlantilla,
                idFutbolista = solicitud.idFutbolista,
                futbolistaTitular = solicitud.futbolistaTitular
            };

            _plantillaFutbolistaRepository.Agregar(asociacion);
            return CreatedAtAction(
                nameof(ObtenerFutbolista),
                new { idPlantilla, idFutbolista = solicitud.idFutbolista },
                asociacion);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{idPlantilla}/futbolistas/{idFutbolista}")]
    public ActionResult<PlantillaFutbolista> ObtenerFutbolista(uint idPlantilla, ushort idFutbolista)
    {
        try
        {
            var asociacion = _plantillaFutbolistaRepository.ObtenerPorId(idPlantilla, idFutbolista);
            if (asociacion is null)
                return NotFound(new { error = "El futbolista no está asociado a esta plantilla." });

            return Ok(asociacion);
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
