using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Aplicacion.Servicios;

public sealed class EquipoServicio(IEquipoRepositorio equipos)
{
    public Task<IReadOnlyList<Equipo>> ListarAsync(CancellationToken ct = default) => equipos.ListarAsync(ct);
}