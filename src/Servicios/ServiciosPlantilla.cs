using System;
using System.Collections.Generic;
using System.Linq;
using Biblio;
using Biblio.IRepo;

namespace Servicios ;

public class ServiciosPlantilla
{
    private readonly IRepoPlantilla repository;

    public ServiciosPlantilla(IRepoPlantilla repository)
    {
        this.repository = repository;
    }

    public List<Plantilla> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Plantilla? ObtenerPorNombre(string nombre)
    {
        return repository.ObtenerPorNombre(nombre);
    }

    public Plantilla Agregar(Plantilla plantilla)
    {
        bool tieneDuplicados = plantilla.detalles
            .Where(detalle => detalle.futbolista != null)
            .Select(detalle => detalle.futbolista!)
            .GroupBy(futbolista => new
            {
                futbolista.nombre,
                futbolista.apellido,
                AnioNacimiento = futbolista.fechaNac?.Year
            })
            .Any(grupo => grupo.Count() > 1);

        if (tieneDuplicados)
        {
            throw new ArgumentException(
                "No se puede agregar la plantilla: contiene jugadores duplicados.");
        }

        return repository.Agregar(plantilla);
    }

    public bool Eliminar(string nombre)
    {
        return repository.Eliminar(nombre);
    }
}

public class ServicePlantilla : ServiciosPlantilla
{
    public ServicePlantilla(IRepoPlantilla repository) : base(repository)
    {
    }
}