using GranDT.Aplicacion.IRepo;
using GranDT.Dominio;

namespace GranDT.Aplicacion.Servicios;

public sealed class PlantillaFutbolistaServicio(
    IPlantillaRepositorio plantillas,
    IFutbolistaRepositorio futbolistas,
    IPlantillaFutbolistaRepositorio integrantes)
{
    public async Task<Plantilla> AgregarAsync(long idPlantilla, long idFutbolista, bool titular, CancellationToken ct = default)
    {
        var plantilla = await plantillas.ObtenerAsync(idPlantilla, ct) ?? throw new RecursoNoEncontradoException("La plantilla no existe.");
        var futbolista = await futbolistas.ObtenerAsync(idFutbolista, ct) ?? throw new RecursoNoEncontradoException("El futbolista no existe.");
        if (await integrantes.ExisteAsync(idPlantilla, idFutbolista, ct)) throw new ConflictoException("El futbolista ya pertenece a la plantilla.");
        if (await integrantes.ContarAsync(idPlantilla, ct) >= plantilla.CantFutbolista)
            throw new ConflictoException("La plantilla alcanzó su cantidad máxima de futbolistas.");
        if (plantilla.CostoActual + futbolista.Cotizacion > plantilla.Presupuesto)
            throw new ReglaNegocioException("La incorporación supera el presupuesto de la plantilla.");
        await integrantes.AgregarAsync(idPlantilla, idFutbolista, titular, ct);
        return await plantillas.ObtenerAsync(idPlantilla, ct) ?? throw new RecursoNoEncontradoException("La plantilla no existe.");
    }

    public async Task<PlantillaFutbolista> CambiarCondicionAsync(long idPlantilla, long idFutbolista, bool titular, CancellationToken ct = default)
    {
        var plantilla = await plantillas.ObtenerAsync(idPlantilla, ct) ?? throw new RecursoNoEncontradoException("La plantilla no existe.");
        var detalle = plantilla.Detalles.FirstOrDefault(d => d.Futbolista.IdFutbolista == idFutbolista)
            ?? throw new RecursoNoEncontradoException("El futbolista no pertenece a la plantilla.");
        await integrantes.CambiarCondicionAsync(idPlantilla, idFutbolista, titular, ct);
        return detalle with { FutbolistaTitular = titular };
    }
}