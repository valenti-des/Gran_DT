using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace GranDTApi.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
     private readonly IRepoUsuario _UsuarioRepository;
        public UsuarioController(IRepoUsuario UsuarioRepository)
        {
            _UsuarioRepository = UsuarioRepository;
        }
        [HttpPost]
        public ActionResult<Usuario> agregar([FromBody] UsuarioCreacion usuario)
        {
            try
            {
                var nuevoUsuario = new Usuario
                {
                    nombre = usuario.nombre,
                    apellido = usuario.apellido,
                    fechaNac = usuario.fechaNac,
                    contraseña = usuario.contraseña,
                    es_admin = usuario.es_admin,
                    email = usuario.email
                };

                var creado = _UsuarioRepository.Agregar(nuevoUsuario);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idUsuario.ToString() }, creado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el Usuario." });
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> ObtenerTodos()
        {
            try{
                return Ok(_UsuarioRepository.ObtenerTodos());
            }
            catch (Exception)
            {
                return StatusCode(500, new { error = "Ocurrió un error al crear el Usuario." });
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> ObtenerPorId(byte idUsuario)
        {
            Usuario? usuario = _UsuarioRepository.ObtenerPorId(idUsuario);
            if (usuario == null)
            {
                return NotFound(new { error = "El usuario no existe." });
            }
            return Ok(usuario);
        }
        
}
