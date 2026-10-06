using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Biblio;
using Biblio.IRepo;

namespace Servicios;

public sealed record CrearFutbolistaRequest(
    [Required(ErrorMessage = "El nombre es obligatorio")] string Nombre,
    [Required(ErrorMessage = "El apellido es obligatorio")] string Apellido,
    string? Apodo,
    DateOnly? FechaNacimiento,
    decimal? Cotizacion,
    byte? IdEquipo,
    byte? IdTipoFutbolista);

public sealed record FutbolistaResponse(
    ushort IdFutbolista,
    string Nombre,
    string Apellido,
    string? Apodo,
    DateOnly? FechaNacimiento,
    decimal? Cotizacion,
    byte? IdEquipo,
    byte? IdTipoFutbolista,
    IReadOnlyList<PuntuacionResponse> Puntuaciones);

public sealed record PuntuacionResponse(
    uint IdPuntuacion,
    float? Puntuacion,
    DateOnly? Fecha);

public interface IServiciosFutbolista
{
    List<FutbolistaResponse> ObtenerTodos();
    FutbolistaResponse? ObtenerPorNombre(string nombre);
    FutbolistaResponse Agregar(CrearFutbolistaRequest request);
    bool Eliminar(string nombre);
}

public class ServiciosFutbolista : IServiciosFutbolista
{
    private readonly IRepoFutbolista _repository;

    public ServiciosFutbolista(IRepoFutbolista repository)
    {
        _repository = repository;
    }

    public List<FutbolistaResponse> ObtenerTodos()
    {
        return _repository.ObtenerTodos().Select(MapearResponse).ToList();
    }

    public FutbolistaResponse? ObtenerPorNombre(string nombre)
    {
        var futbolista = _repository.ObtenerPorNombre(nombre);
        return futbolista is null ? null : MapearResponse(futbolista);
    }

    public FutbolistaResponse Agregar(CrearFutbolistaRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var nuevoFutbolista = new Futbolista
        {
            nombre = request.Nombre,
            apellido = request.Apellido,
            apodo = request.Apodo,
            fechaNac = request.FechaNacimiento,
            cotizacion = request.Cotizacion,
            idEquipo = request.IdEquipo,
            idTipoFutbolista = request.IdTipoFutbolista
        };

        return MapearResponse(_repository.Agregar(nuevoFutbolista));
    }

    public bool Eliminar(string nombre)
    {
        return _repository.Eliminar(nombre);
    }

    private static FutbolistaResponse MapearResponse(Futbolista futbolista)
    {
        return new FutbolistaResponse(
            futbolista.idFutbolista,
            futbolista.nombre,
            futbolista.apellido,
            futbolista.apodo,
            futbolista.fechaNac,
            futbolista.cotizacion,
            futbolista.idEquipo,
            futbolista.idTipoFutbolista,
            futbolista.puntuaciones?
                .Select(puntuacion => new PuntuacionResponse(
                    puntuacion.idPuntuacion,
                    puntuacion.puntuacion,
                    puntuacion.cantFech))
                .ToList() ?? new List<PuntuacionResponse>());
    }
}