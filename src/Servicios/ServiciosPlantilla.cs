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

    public Plantilla? ObtenerPorid(ushort id)
    {
        return repository.ObtenerPorId(id);
    }
//cantidad maxima de futbolistas por plantilla es de 20
//agregar un limite futbolistas por plantilla para que no se pueda agregar mas de lo permitido dentro del servicios
    public Plantilla Agregar(Plantilla Plantilla)
    {
        return repository.Agregar(Plantilla);
    }

    public bool Eliminar(ushort id)
    {
        return repository.Eliminar(id);
    }
}