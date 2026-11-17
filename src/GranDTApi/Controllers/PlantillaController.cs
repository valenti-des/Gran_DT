using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace GranDTApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PlantillaController : ControllerBase
{
    private readonly IRepoPlantilla _PlantillaRepository;
    private readonly IRepoPlantillaFutbolista _plantillaFutbolistaRepository;

    public PlantillaController(
        IRepoPlantilla PlantillaRepository,
        IRepoPlantillaFutbolista plantillaFutbolistaRepository)
    {
        _PlantillaRepository = PlantillaRepository;
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
                    idUsuario = plantilla.idUsuario
                };

                var creado = _PlantillaRepository.Agregar(nuevoPlantilla);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idPlantilla.ToString() }, creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el Equipo." });
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Plantilla>> ObtenerTodos()
        {
            try{
                return Ok(_PlantillaRepository.ObtenerTodos());
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el Equipo." });
            }
        }

        [HttpGet("{idPlantilla}")]
        public ActionResult<Plantilla> ObtenerPorId(byte idPlantilla)
        {
            Plantilla? plantilla = _PlantillaRepository.ObtenerPorId(idPlantilla);
            if (plantilla == null)
            {
                return NotFound(new { error = "La plantilla no existe." });
            }
            return Ok(plantilla);
        }

        [HttpPost("{idPlantilla}/idfutbolistas")]
        public ActionResult<PlantillaFutbolista> AgregarFutbolista(
            byte idPlantilla,
            [FromBody] PlantillaFutbolistaCreacion solicitud)
        {
            var plantilla = _PlantillaRepository.ObtenerPorId(idPlantilla);
            if (plantilla is null)
                return NotFound(new { error = "La plantilla no existe." });

            if (_plantillaFutbolistaRepository.ObtenerPorId(idPlantilla, solicitud.idFutbolista) is not null)
                return Conflict(new { error = "El futbolista ya está asociado a esta plantilla." });

            if (plantilla.cantMaxFutbolista is byte cantidadMaxima
                && plantilla.detalles.Count >= cantidadMaxima)
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

        [HttpGet("{idPlantilla}/futbolistas/{idFutbolista}")]
        public ActionResult<PlantillaFutbolista> ObtenerFutbolista(byte idPlantilla, ushort idFutbolista)
        {
            var asociacion = _plantillaFutbolistaRepository.ObtenerPorId(idPlantilla, idFutbolista);
            if (asociacion is null)
                return NotFound(new { error = "El futbolista no está asociado a esta plantilla." });

            return Ok(asociacion);
        }

        [HttpDelete("{idPlantilla}")]
        public ActionResult Eliminar(ushort idPlantilla)
        {
            try
            {
                var plantilla = _PlantillaRepository.ObtenerPorId(idPlantilla);
                if (plantilla is null)
                    return NotFound(new { error = "La plantilla no existe." });

                var eliminado = _PlantillaRepository.Eliminar(idPlantilla);
                if (!eliminado)
                    return StatusCode(500, new { error = "No se pudo eliminar la plantilla." });

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al eliminar la plantilla." });
            }
        }
    
}
