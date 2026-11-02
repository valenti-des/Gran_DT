using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace GranDTApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class PuntuacionController : ControllerBase
{
     private readonly IRepoPuntuacion _PuntuacionRepository;
        public PuntuacionController(IRepoPuntuacion PuntuacionRepository)
        {
            _PuntuacionRepository = PuntuacionRepository;
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
                    numFech = puntuacion.numFech
                };

                var creado = _PuntuacionRepository.Agregar(nuevaPuntuacion);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idPuntuacion.ToString() }, creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear la Puntuación." });
            }
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<Puntuacion>> ObtenerTodos()
        {
            try{
                return Ok(_PuntuacionRepository.ObtenerTodos());
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear la Puntuación." });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Puntuacion> ObtenerPorId(byte idPuntuacion)
        {
            Puntuacion? puntuacion = _PuntuacionRepository.ObtenerPorId(idPuntuacion);
            if (puntuacion == null)
            {
                return NotFound(new { error = "La puntuación no existe." });
            }
            return Ok(puntuacion);
        }
        
}
