using Biblio; // Ajusta según el namespace de tu modelo Futbolista
using Biblio.IRepo;
using Microsoft.AspNetCore.Mvc;

namespace GranDTApi.Controllers
{
    [ApiController]
    [Route("futbolistas")]
    public class FutbolistasController : ControllerBase
    {
        private readonly IRepoFutbolista _futbolistaController;

        public FutbolistasController(IRepoFutbolista futbolistaService)
        {
            _futbolistaController = futbolistaService;
        }

        [HttpPost]
        public ActionResult<Futbolista> agregar([FromBody] FutbolistaCreacion futbolista)
        {
            try
            {
                var creado = _futbolistaController.Agregar(futbolista);
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
                return Ok(_futbolistaController.ObtenerTodos());
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el futbolista." });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Futbolista> ObtenerPorId(string id)
        {
            Futbolista? futbolista = _futbolistaController.ObtenerPorId(id);
            if (futbolista == null)
            {
                return NotFound(new { error = "El futbolista no existe." });
            }
            return Ok(futbolista);
        }
    }
}