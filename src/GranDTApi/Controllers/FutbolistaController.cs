using Biblio; // Ajusta según el namespace de tu modelo Futbolista
using Biblio.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace GranDT_ET12API.Controllers
{
    [ApiController]
    [Route("futbolistas")]
    public class FutbolistasController : ControllerBase
    {
        private readonly IRepoFutbolista _futbolistaService;

        public FutbolistasController(IRepoFutbolista futbolistaService)
        {
            _futbolistaService = futbolistaService;
        }

        [HttpPost]
        public ActionResult<Futbolista> Crear([FromBody] FutbolistaCreacion futbolista)
        {
            try
            {
                var creado = _futbolistaService.Agregar(futbolista);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idFutbolista.ToString() }, creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el futbolista." });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Futbolista>> ObtenerTodos()
        {
            try{
                return Ok(_futbolistaService.ObtenerTodos());
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el futbolista." });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Futbolista> ObtenerPorId(string id)
        {
            Futbolista? futbolista = _futbolistaService.ObtenerPorId(id);
            if (futbolista == null)
            {
                return NotFound(new { error = "El futbolista no existe." });
            }
            return Ok(futbolista);
        }
    }
}