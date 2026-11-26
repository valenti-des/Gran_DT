using Biblio;
using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers
{
    [ApiController]
    [Route("futbolistas")]
    public class FutbolistasController : ControllerBase
    {
        private readonly ServiciosFutbolista _serviciosFutbolista;

        public FutbolistasController(ServiciosFutbolista serviciosFutbolista)
        {
            _serviciosFutbolista = serviciosFutbolista;
        }

        [HttpPost]
        public ActionResult<Futbolista> Agregar([FromBody] FutbolistaCreacion futbolista)
        {
            try
            {
                var nuevoFutbolista = new Futbolista
                {
                    nombre = futbolista.nombre,
                    apellido = futbolista.apellido,
                    apodo = futbolista.apodo,
                    fechaNac = futbolista.fechaNac,
                    cotizacion = futbolista.cotizacion,
                    idEquipo = futbolista.idEquipo,
                    idTipoFutbolista = futbolista.idTipoFutbolista,
                   
                };

                var creado = _serviciosFutbolista.Agregar(nuevoFutbolista);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idFutbolista }, creado);
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
        public ActionResult<IEnumerable<Futbolista>> ObtenerTodos()
        {
            try
            {
                return Ok(_serviciosFutbolista.ObtenerTodos());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Futbolista> ObtenerPorId(ushort id)
        {
            try
            {
                var futbolista = _serviciosFutbolista.ObtenerPorId(id);
                if (futbolista == null)
                {
                    return NotFound(new { error = "El futbolista no existe." });
                }

                return Ok(futbolista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Eliminar(ushort id)
        {
            try
            {
                var futbolista = _serviciosFutbolista.ObtenerPorId(id);
                if (futbolista is null)
                    return NotFound(new { error = "El futbolista no existe." });

                var eliminado = _serviciosFutbolista.Eliminar(id);
                if (!eliminado)
                    return StatusCode(500, new { error = "No se pudo eliminar el futbolista." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}