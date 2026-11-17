using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosPlantilla
{
    private readonly IRepoPlantilla _repository;
    private readonly IRepoPlantillaFutbolista _plantillaFutbolistaRepository;

    public ServiciosPlantilla(
        IRepoPlantilla repository,
        IRepoPlantillaFutbolista plantillaFutbolistaRepository)
    {
        _repository = repository;
        _plantillaFutbolistaRepository = plantillaFutbolistaRepository;
    }

    public List<Plantilla> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Plantilla? ObtenerPorId(uint id)
    {
        return _repository.ObtenerPorId(id);
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        ArgumentNullException.ThrowIfNull(plantilla);

        ValidarSi(
            string.IsNullOrWhiteSpace(plantilla.nombreP),
            () => new ArgumentException("La plantilla debe tener un nombre.", nameof(plantilla)));
        ValidarSi(
            plantilla.presupuesto < 0m,
            () => new ArgumentException("El presupuesto no puede ser negativo.", nameof(plantilla)));
        ValidarSi(
            plantilla.cantFutbolista > 20,
            () => new ArgumentException("La cantidad máxima de futbolistas por plantilla debe ser entre 1 y 20.", nameof(plantilla)));
        plantilla.nombreP = plantilla.nombreP.Trim();
        return _repository.Agregar(plantilla);
    }

    public bool Eliminar(uint id)
    {
        return _repository.Eliminar(id);
    }

    public PlantillaFutbolista? ObtenerFutbolista(uint idPlantilla, ushort idFutbolista)
    {
        return _plantillaFutbolistaRepository.ObtenerPorId(idPlantilla, idFutbolista);
    }

    public PlantillaFutbolista AgregarFutbolista(
        uint idPlantilla,
        ushort idFutbolista,
        bool futbolistaTitular)
    {
        var plantilla = _repository.ObtenerPorId(idPlantilla)
            ?? throw new KeyNotFoundException("La plantilla no existe.");

        ValidarSi(
            _plantillaFutbolistaRepository.ObtenerPorId(idPlantilla, idFutbolista) is not null,
            () => new InvalidOperationException("El futbolista ya está asociado a esta plantilla."));

        const byte cantidadMaxima = 20;
        ValidarSi(
            plantilla.detalles.Count >= cantidadMaxima,
            () => new InvalidOperationException("La plantilla alcanzó su cantidad máxima de futbolistas."));

        var asociacion = new PlantillaFutbolista
        {
            idPlantilla = idPlantilla,
            idFutbolista = idFutbolista,
            futbolistaTitular = futbolistaTitular
        };


        return _plantillaFutbolistaRepository.Agregar(asociacion);


        
    }

    private static void ValidarSi(bool condicion, Func<Exception> crearExcepcion)
    {
        if (condicion)
        {
            throw crearExcepcion();
        }
    }
}