using Biblio;
using Biblio.IRepo;

namespace Servicios;

public interface IEquipoService
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorId(byte id);
    Equipo? Crear(Equipo equipo);
    bool Eliminar(string nombre);
}

public sealed class EquipoService : IEquipoService
{
    private readonly IRepoEquipo _equipoRepository;

    public EquipoService(IRepoEquipo equipoRepository)
    {
        _equipoRepository = equipoRepository;
    }

    public List<Equipo> ObtenerTodos() => _equipoRepository.ObtenerTodos();

    public Equipo? ObtenerPorId(byte id) => _equipoRepository.ObtenerPorId(id);

    public bool Eliminar(string nombre)
    {
        if (string.IsNullOrWhiteSpace(nombre))
        {
            return false;
        }

        return _equipoRepository.Eliminar(nombre.Trim());
    }

    public Equipo? Crear(Equipo equipo)
    {
        ArgumentNullException.ThrowIfNull(equipo);

        if (string.IsNullOrWhiteSpace(equipo.nombre))
        {
            throw new ArgumentException("El nombre del equipo es obligatorio.", nameof(equipo));
        }

        equipo.nombre = equipo.nombre.Trim();

        if (_equipoRepository.ObtenerPorNombre(equipo.nombre) is not null)
        {
            return null;
        }

        return _equipoRepository.Agregar(equipo);
    }
}