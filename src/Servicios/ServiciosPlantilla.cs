using System.Collections.Generic;
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

    public Plantilla Agregar(Plantilla Plantilla)
    {
        return repository.Agregar(Plantilla);
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