using Microsoft.AspNetCore.Mvc;
using Servicios;

namespace GranDTApi.Controllers;

/// <summary>Endpoints para gestionar futbolistas.</summary>
[ApiController]
[Route("api/[controller]")]
public class FutbolistaController(IServiciosFutbolista futbolistaService) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<FutbolistaResponse>> ObtenerTodos()
    {
        // El controlador ya recibe la lista formateada (Response) desde el servicio
        var futbolistas = futbolistaService.ObtenerTodos();
        return Ok(futbolistas);
    }

    [HttpGet("{nombre}")]
    public ActionResult<FutbolistaResponse> ObtenerPorNombre(string nombre)
    {
        var futbolista = futbolistaService.ObtenerPorNombre(nombre);

        if (futbolista is null)
            return NotFound();

        return Ok(futbolista);
    }

    [HttpDelete("{nombre}")]
    public IActionResult Eliminar(string nombre)
    {
        // Ya no necesitas validar IsNullOrWhiteSpace aquí. ASP.NET MVC model binding
        // te protegerá de rutas vacías en [HttpDelete("{nombre}")], y si llega a fallar,
        // el servicio puede manejar validaciones de negocio más complejas.
        var eliminado = futbolistaService.Eliminar(nombre);

        if (!eliminado)
            return NotFound();

        return NoContent(); // 204
    }

    [HttpPost]
    public ActionResult<FutbolistaResponse> Crear([FromBody] CrearFutbolistaRequest request)
    {
        // Como usamos Data Annotations ([Required]) en el DTO, ASP.NET devuelve un
        // 400 Bad Request automáticamente si falta el Nombre o Apellido.
        // Ya no necesitamos los if(string.IsNullOrWhiteSpace) manuales.

        var nuevoFutbolistaResponse = futbolistaService.Agregar(request);

        return CreatedAtAction(
            nameof(ObtenerPorNombre),
            new { nombre = nuevoFutbolistaResponse.Nombre },
            nuevoFutbolistaResponse); // 201
    }
}