using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosEquipo
{
    private readonly IRepoEquipo _repository;

    public ServiciosEquipo(IRepoEquipo repository)
    {
        _repository = repository;
    }

    public List<Equipo> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Equipo? ObtenerPorId(byte idEquipo)
    {
        return _repository.ObtenerPorId(idEquipo);
    }

    public Equipo Agregar(Equipo equipo)
    {
        ArgumentNullException.ThrowIfNull(equipo);

        if (string.IsNullOrWhiteSpace(equipo.nombre))
        {
            throw new ArgumentException("El nombre del equipo es obligatorio.", nameof(equipo));
        }

        equipo.nombre = equipo.nombre.Trim();

        bool yaExiste = _repository.ObtenerTodos().Any(e =>
            string.Equals(e.nombre.Trim(), equipo.nombre, StringComparison.OrdinalIgnoreCase));

        if (yaExiste)
        {
            throw new ArgumentException("Ya existe un equipo con ese nombre.", nameof(equipo));
        }
        return _repository.Agregar(equipo);
    }

    public bool Eliminar(byte idEquipo)
    {
        return _repository.Eliminar(idEquipo);
    }
}