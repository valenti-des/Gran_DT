using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Aplicacion.Servicios;

public sealed class PlantillaServicio(IPlantillaRepositorio plantillas, IUsuarioRepositorio usuarios)
{
    public async Task<IReadOnlyList<Plantilla>> ListarDeUsuarioAsync(long idUsuario, CancellationToken ct = default)
    {
        if (await usuarios.ObtenerAsync(idUsuario, ct) is null) throw new RecursoNoEncontradoException("El usuario no existe.");
        return await plantillas.ListarDeUsuarioAsync(idUsuario, ct);
    }

    public async Task<Plantilla> CrearAsync(long idUsuario, CrearPlantilla solicitud, CancellationToken ct = default)
    {
        if (await usuarios.ObtenerAsync(idUsuario, ct) is null) throw new RecursoNoEncontradoException("El usuario no existe.");
        _ = new Plantilla(0, solicitud.NombreP, solicitud.Presupuesto, solicitud.CantFutbolista, idUsuario);
        return await plantillas.CrearAsync(solicitud.NombreP, solicitud.Presupuesto, solicitud.CantFutbolista, idUsuario, ct);
    }

    public async Task<Plantilla> ObtenerAsync(long id, CancellationToken ct = default) =>
        await plantillas.ObtenerAsync(id, ct) ?? throw new RecursoNoEncontradoException("La plantilla no existe.");

    public async Task<IReadOnlyList<string>> ValidarAsync(long id, CancellationToken ct = default) => (await ObtenerAsync(id, ct)).Validar();

    public async Task<decimal> PuntajeAsync(long id, int nroFecha, CancellationToken ct = default)
    {
        if (nroFecha is < 1 or > 49) throw new ReglaNegocioException("La fecha debe estar entre 1 y 49.");
        return (await ObtenerAsync(id, ct)).PuntajeFecha(nroFecha);
    }
}