using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Aplicacion.Servicios;

public sealed class FutbolistaServicio(IFutbolistaRepositorio futbolistas, IEquipoRepositorio equipos)
{
    public Task<IReadOnlyList<Futbolista>> ListarAsync(CancellationToken ct = default) => futbolistas.ListarAsync(ct);

    public async Task<Futbolista> ObtenerAsync(long id, CancellationToken ct = default) =>
        await futbolistas.ObtenerAsync(id, ct) ?? throw new RecursoNoEncontradoException("El futbolista no existe.");

    public async Task<Futbolista> CrearAsync(CrearFutbolista solicitud, CancellationToken ct = default)
    {
        Futbolista.ValidarDatos(solicitud.Nombre, solicitud.Apellido, solicitud.Cotizacion, solicitud.IdEquipo, solicitud.Tipo);
        if (!await equipos.ExisteAsync(solicitud.IdEquipo, ct)) throw new RecursoNoEncontradoException("El equipo no existe.");
        var puntuaciones = solicitud.Puntuaciones ?? [];
        foreach (var puntuacion in puntuaciones) Puntuacion.Validar(puntuacion.Valor, puntuacion.NroFecha);
        if (puntuaciones.GroupBy(p => p.NroFecha).Any(g => g.Count() > 1))
            throw new ConflictoException("El futbolista no puede tener dos puntuaciones en la misma fecha.");

        var futbolista = new Futbolista(0, solicitud.Nombre.Trim(), solicitud.Apellido.Trim(), solicitud.Apodo,
            solicitud.FechaNac, solicitud.Cotizacion, solicitud.IdEquipo, solicitud.Tipo, []);
        var nuevasPuntuaciones = puntuaciones.Select(p => (p.Valor, (byte)p.NroFecha)).ToArray();
        return await futbolistas.CrearAsync(futbolista, nuevasPuntuaciones, ct);
    }

    public async Task<Futbolista> ActualizarAsync(long id, ActualizarFutbolista solicitud, CancellationToken ct = default)
    {
        var existente = await ObtenerAsync(id, ct);
        Futbolista.ValidarDatos(solicitud.Nombre, solicitud.Apellido, solicitud.Cotizacion, solicitud.IdEquipo, solicitud.Tipo);
        if (!await equipos.ExisteAsync(solicitud.IdEquipo, ct)) throw new RecursoNoEncontradoException("El equipo no existe.");
        var actualizado = existente with
        {
            Nombre = solicitud.Nombre.Trim(), Apellido = solicitud.Apellido.Trim(), Apodo = solicitud.Apodo,
            FechaNac = solicitud.FechaNac, Cotizacion = solicitud.Cotizacion,
            IdEquipo = solicitud.IdEquipo, Tipo = solicitud.Tipo
        };
        return await futbolistas.ActualizarAsync(actualizado, ct) ?? throw new RecursoNoEncontradoException("El futbolista no existe.");
    }
}