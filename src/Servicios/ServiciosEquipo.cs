using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios ;

public class ServiceEquipo
{
    private readonly IRepoEquipo repository;

    public ServiceEquipo(IRepoEquipo repository)
    {
        this.repository = repository;
    }

    public List<Equipo> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Equipo? ObtenerPorid(byte idEquipo)
    {
        return repository.ObtenerPorId(idEquipo);
    }

    public Equipo Agregar(Equipo equipo)
    {
        return repository.Agregar(equipo);
    }

    public bool Eliminar(byte idEquipo)
    {
        return repository.Eliminar(idEquipo);
    }
}