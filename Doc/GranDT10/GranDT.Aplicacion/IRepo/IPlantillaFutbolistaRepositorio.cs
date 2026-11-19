namespace GranDT.Aplicacion.IRepo;

public interface IPlantillaFutbolistaRepositorio
{
    Task<bool> ExisteAsync(long idPlantilla, long idFutbolista, CancellationToken cancellationToken = default);
    Task<int> ContarAsync(long idPlantilla, CancellationToken cancellationToken = default);
    Task AgregarAsync(long idPlantilla, long idFutbolista, bool titular, CancellationToken cancellationToken = default);
    Task<bool> CambiarCondicionAsync(long idPlantilla, long idFutbolista, bool titular, CancellationToken cancellationToken = default);
}