using Microsoft.AspNetCore.Mvc;
using Biblio;     // Para acceder al modelo Equipo
using Servicios;  // Para acceder a EquipoService

namespace GranDTApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EquipoController : ControllerBase
{
    private readonly EquipoService _equipoService;

    // Inyección del Servicio mediante el constructor
    public EquipoController(EquipoService equipoService)
    {
        _equipoService = equipoService;
    }

    // GET: api/equipo
    [HttpGet]
    public IActionResult ObtenerTodos()
    {
        var equipos = _equipoService.ObtenerTodos();
        return Ok(equipos); // Devuelve HTTP 200 OK con el listado JSON
    }

    // GET: api/equipo/5
    [HttpGet("{id}")]
    public IActionResult ObtenerPorId(int id)
    {
        var equipo = _equipoService.ObtenerPorId(id);

        if (equipo == null)
        {
            return NotFound(new { mensaje = $"No se encontró el equipo con ID {id}" }); // HTTP 404
        }

        return Ok(equipo); // HTTP 200 OK
    }

    // POST: api/equipo
    [HttpPost]
    public IActionResult Crear([FromBody] Equipo equipo)
    {
        if (equipo == null)
        {
            return BadRequest(new { mensaje = "Los datos del equipo son inválidos." }); // HTTP 400
        }

        bool creado = _equipoService.Crear(equipo);

        if (!creado)
        {
            return BadRequest(new { mensaje = "No se pudo crear el equipo. Verifique los datos o si el nombre está duplicado." });
        }

        return CreatedAtAction(nameof(ObtenerPorId), new { id = equipo.IdEquipo }, equipo); // HTTP 201 Created
    }
}

