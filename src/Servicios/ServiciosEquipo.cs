using System.Collections.Generic;
using System.Threading.Channels;
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
        if (equipo.nombre == "")
        {
           throw new ArgumentException("El nombre del equipo es obligatorio.", nameof(equipo));
        }

        if (string.IsNullOrWhiteSpace(equipo.nombre))
        {
            throw new ArgumentException("El nombre del equipo es obligatorio.", nameof(equipo));
        }

        equipo.nombre = equipo.nombre.Trim();

        bool yaExiste = repository.ObtenerTodos().Any(e =>
            string.Equals(e.nombre.Trim(), equipo.nombre, StringComparison.OrdinalIgnoreCase));

        if (yaExiste)
        {
            throw new ArgumentException("Ya existe un equipo con ese nombre.", nameof(equipo));
        }
        return repository.Agregar(equipo);
    }

    public bool Eliminar(byte idEquipo)
    {
        return repository.Eliminar(idEquipo);
    }
}