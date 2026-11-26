using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosPlantilla
{
    private readonly IRepoPlantilla _repository;

    public ServiciosPlantilla(IRepoPlantilla repository)
    {
        _repository = repository;
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

    public bool Eliminar(uint id, ushort idUsuario)
    {
        var plantilla = _repository.ObtenerPorId(id);
        if (plantilla is null || plantilla.idUsuario != idUsuario)
            return false;

        return _repository.Eliminar(id);
    }

    private static void ValidarSi(bool condicion, Func<Exception> crearExcepcion)
    {
        if (condicion)
        {
            throw crearExcepcion();
        }
    }
}