using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios ;

public class ServiciosFutbolista
{
    private readonly IRepoFutbolista repository;

    public ServiciosFutbolista(IRepoFutbolista repository)
    {
        this.repository = repository;
    }

    public List<Futbolista> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Futbolista? ObtenerPorNombre(string nombre)
    {
        return repository.ObtenerPorNombre(nombre);
    }

    public Futbolista Agregar(Futbolista futbolista)
    {
        return repository.Agregar(futbolista);
    }

    public bool Eliminar(string nombre)
    {
        return repository.Eliminar(nombre);
    }
}