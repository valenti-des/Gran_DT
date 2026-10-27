using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace GranDTApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
     private readonly IRepoEquipo _EquipoRepository;
        public EquipoController(IRepoEquipo equipoRepository)
        {
            _EquipoRepository = equipoRepository;
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

                var creado = _EquipoRepository.Agregar(nuevoEquipo);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idEquipo.ToString() }, creado);
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
        public ActionResult<IEnumerable<Equipo>> ObtenerTodos()
        {
            try{
                return Ok(_EquipoRepository.ObtenerTodos());
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el Equipo." });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Equipo> ObtenerPorId(byte idEquipo)
        {
            Equipo? equipo = _EquipoRepository.ObtenerPorId(idEquipo);
            if (equipo == null)
            {
                return NotFound(new { error = "El equipo no existe." });
            }
            return Ok(equipo);
        }
        
}
