using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Aplicacion.Servicios;

public sealed class PuntuacionServicio(IPuntuacionRepositorio puntuaciones, IFutbolistaRepositorio futbolistas)
{
    public async Task<Puntuacion> CrearAsync(long idFutbolista, decimal valor, int nroFecha, CancellationToken ct = default)
    {
        Puntuacion.Validar(valor, nroFecha);
        if (await futbolistas.ObtenerAsync(idFutbolista, ct) is null) throw new RecursoNoEncontradoException("El futbolista no existe.");
        if (await puntuaciones.ExisteEnFechaAsync(idFutbolista, (byte)nroFecha, ct))
            throw new ConflictoException("Ya existe una puntuación para ese futbolista y fecha.");
        return await puntuaciones.CrearAsync(idFutbolista, valor, (byte)nroFecha, ct);
    }
}