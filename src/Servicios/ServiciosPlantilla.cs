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

        if (string.IsNullOrWhiteSpace(plantilla.nombreP))
        {
            throw new ArgumentException("La plantilla debe tener un nombre.", nameof(plantilla));
        }

        if (plantilla.presupuesto < 0m)
        {
            throw new ArgumentException("El presupuesto no puede ser negativo.", nameof(plantilla));
        }

        if (plantilla.cantFutbolista > 20)
        {
            throw new ArgumentException("La cantidad máxima de futbolistas por plantilla debe ser entre 1 y 20.", nameof(plantilla));
        }

        plantilla.nombreP = plantilla.nombreP.Trim();
        return _repository.Agregar(plantilla);
    }

    public bool Eliminar(uint id)
    {
        return _repository.Eliminar(id);
    }
}