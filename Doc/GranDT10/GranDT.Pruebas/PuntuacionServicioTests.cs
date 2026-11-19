using GranDT.Aplicacion.IRepo;
using GranDT.Aplicacion.Servicios;
using GranDT.Dominio;
using Xunit;

namespace GranDT.Pruebas;

public sealed class PuntuacionServicioTests
{
    [Fact]
    public async Task RechazaDosPuntuacionesEnLaMismaFecha()
    {
        var futbolista = new Futbolista(1, "Nombre", "Apellido", null, null, 1, 1, TipoFutbolista.Arquero, []);
        var servicio = new PuntuacionServicio(new PuntuacionesExistentes(), new FutbolistasExistentes(futbolista));

        await Assert.ThrowsAsync<ConflictoException>(() => servicio.CrearAsync(1, 8m, 4));
    }

    private sealed class FutbolistasExistentes(Futbolista futbolista) : IFutbolistaRepositorio
    {
        public Task<IReadOnlyList<Futbolista>> ListarAsync(CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<Futbolista>>([futbolista]);
        public Task<Futbolista?> ObtenerAsync(long id, CancellationToken cancellationToken = default) => Task.FromResult<Futbolista?>(id == futbolista.IdFutbolista ? futbolista : null);
        public Task<Futbolista> CrearAsync(Futbolista dato, IReadOnlyCollection<(decimal Valor, byte NroFecha)> puntuaciones, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<Futbolista?> ActualizarAsync(Futbolista dato, CancellationToken cancellationToken = default) => throw new NotImplementedException();
    }

    private sealed class PuntuacionesExistentes : IPuntuacionRepositorio
    {
        public Task<Puntuacion> CrearAsync(long idFutbolista, decimal valor, byte nroFecha, CancellationToken cancellationToken = default) => throw new NotImplementedException();
        public Task<IReadOnlyList<Puntuacion>> ListarDeFutbolistaAsync(long idFutbolista, CancellationToken cancellationToken = default) => Task.FromResult<IReadOnlyList<Puntuacion>>([]);
        public Task<bool> ExisteEnFechaAsync(long idFutbolista, byte nroFecha, CancellationToken cancellationToken = default) => Task.FromResult(true);
    }
}
