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
        public PlantillaController(IRepoPlantilla PlantillaRepository)
        {
            _PlantillaRepository = PlantillaRepository;
        }
        [HttpPost]
        public ActionResult<Plantilla> agregar([FromBody] PlantillaCreacion plantilla)
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

        [HttpGet("{id}")]
        public ActionResult<Plantilla> ObtenerPorId(byte idPlantilla)
        {
            Plantilla? plantilla = _PlantillaRepository.ObtenerPorId(idPlantilla);
            if (plantilla == null)
            {
                return NotFound(new { error = "La plantilla no existe." });
            }
            return Ok(plantilla);
        }
        
}
