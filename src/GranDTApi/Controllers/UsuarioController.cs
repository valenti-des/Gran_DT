using Microsoft.AspNetCore.Mvc;
using Biblio;
using Servicios;

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly ServiciosUsuario _serviciosUsuario;

    public UsuarioController(ServiciosUsuario serviciosUsuario)
    {
        _serviciosUsuario = serviciosUsuario;
    }

    [HttpPost]
    public ActionResult<Usuario> Agregar([FromBody] UsuarioCreacion usuario)
    {
        try
        {
            var nuevoUsuario = new Usuario
            {
                nombre = usuario.nombre,
                apellido = usuario.apellido,
                fechaNac = usuario.fechaNac,
                contrasena = usuario.contrasena,
                es_admin = usuario.es_admin,
                email = usuario.email
            };

            var creado = _serviciosUsuario.Agregar(nuevoUsuario);
            return CreatedAtAction(nameof(ObtenerPorId), new { id = creado.idUsuario }, creado);
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
    public ActionResult<IEnumerable<Usuario>> ObtenerTodos()
    {
        try
        {
            return Ok(_serviciosUsuario.ObtenerTodos());
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public ActionResult<Usuario> ObtenerPorId(byte idUsuario)
    {
        try
        {
            var usuario = _serviciosUsuario.ObtenerPorId(idUsuario);
            if (usuario == null)
            {
                return NotFound(new { error = "El usuario no existe." });
            }

            return Ok(usuario);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Eliminar(ushort idUsuario)
    {
        try
        {
            var usuario = _serviciosUsuario.ObtenerPorId(idUsuario);
            if (usuario is null)
                return NotFound(new { error = "El usuario no existe." });

            var eliminado = _serviciosUsuario.Eliminar(idUsuario);
            if (!eliminado)
                return StatusCode(500, new { error = "No se pudo eliminar el usuario." });

            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
