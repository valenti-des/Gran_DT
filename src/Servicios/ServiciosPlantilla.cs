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
        if (string.IsNullOrWhiteSpace(Plantilla.nombreP))
        {
            throw new ArgumentException("La plantilla debe tener un nombre.", nameof(Plantilla));
        }
        
        if (Plantilla.cantMaxMonto < 0)
        {
            throw new ArgumentException("La cantidad máxima de monto no puede ser negativa debe ser valores positiva.", nameof(Plantilla));
        }

        if (Plantilla.cantMaxFutbolista < 1 || Plantilla.cantMaxFutbolista > 20)
        {
            throw new ArgumentException("La cantidad máxima de futbolistas por plantilla debe ser entre 1 y 20.", nameof(Plantilla));
        }

        return repository.Agregar(Plantilla);
         var resultado = repository.Agregar(Plantilla);
        return resultado.nombre;
    }

    public bool Eliminar(ushort id)
    {
        return repository.Eliminar(id);
    }
}