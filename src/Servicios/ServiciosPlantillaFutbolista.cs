using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosPlantillaFutbolista
{
    private const byte CantidadMaximaFutbolistas = 20;

    private readonly IRepoPlantillaFutbolista _repository;
    private readonly IRepoPlantilla _plantillaRepository;
    private readonly IRepoFutbolista _futbolistaRepository;

    public ServiciosPlantillaFutbolista(
        IRepoPlantillaFutbolista repository,
        IRepoPlantilla plantillaRepository,
        IRepoFutbolista futbolistaRepository)
    {
        _repository = repository;
        _plantillaRepository = plantillaRepository;
        _futbolistaRepository = futbolistaRepository;
    }

    public List<PlantillaFutbolista> ObtenerPorPlantilla(uint idPlantilla)
    {
        ValidarIdPlantilla(idPlantilla);

        if (_plantillaRepository.ObtenerPorId(idPlantilla) is null)
            throw new KeyNotFoundException("La plantilla no existe.");

        return _repository.ObtenerPorPlantilla(idPlantilla);
    }

    public PlantillaFutbolista? ObtenerPorId(uint idPlantilla, ushort idFutbolista)
    {
        ValidarIds(idPlantilla, idFutbolista);
        return _repository.ObtenerPorId(idPlantilla, idFutbolista);
    }

    public PlantillaFutbolista Agregar(
        uint idPlantilla,
        ushort idFutbolista,
        bool futbolistaTitular)
    {
        ValidarIds(idPlantilla, idFutbolista);

        var plantilla = _plantillaRepository.ObtenerPorId(idPlantilla)
            ?? throw new KeyNotFoundException("La plantilla no existe.");

        var futbolista = _futbolistaRepository.ObtenerPorId(idFutbolista);
        if (futbolista is null)
            throw new KeyNotFoundException("El futbolista no existe.");

        if (_repository.ObtenerPorId(idPlantilla, idFutbolista) is not null)
            throw new InvalidOperationException("El futbolista ya está asociado a esta plantilla.");

        if (plantilla.detalles.Count >= CantidadMaximaFutbolistas)
            throw new InvalidOperationException("La plantilla alcanzó su cantidad máxima de futbolistas.");

        var asociacion = new PlantillaFutbolista
        {
            idPlantilla = idPlantilla,
            idFutbolista = idFutbolista,
            futbolistaTitular = futbolistaTitular,
            nombreFutbolista = $"{futbolista.nombre} {futbolista.apellido}".Trim()
        };

        return _repository.Agregar(asociacion);
    }

    public bool Eliminar(uint idPlantilla, ushort idFutbolista)
    {
        ValidarIds(idPlantilla, idFutbolista);
        return _repository.Eliminar(idPlantilla, idFutbolista);
    }

    private static void ValidarIds(uint idPlantilla, ushort idFutbolista)
    {
        ValidarIdPlantilla(idPlantilla);

        if (idFutbolista == 0)
            throw new ArgumentOutOfRangeException(nameof(idFutbolista), "El id del futbolista debe ser mayor que cero.");
    }

    private static void ValidarIdPlantilla(uint idPlantilla)
    {
        if (idPlantilla == 0)
            throw new ArgumentOutOfRangeException(nameof(idPlantilla), "El id de la plantilla debe ser mayor que cero.");
    }
}
