using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosFutbolista
{
    private readonly IRepoFutbolista _repository;

    public ServiciosFutbolista(IRepoFutbolista repository)
    {
        _repository = repository;
    }

    public List<Futbolista> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Futbolista? ObtenerPorId(ushort id)
    {
        return _repository.ObtenerPorId(id);
    }

    public Futbolista Agregar(Futbolista futbolista)
    {
        ArgumentNullException.ThrowIfNull(futbolista);

        if (futbolista.fechaNac == default || futbolista.fechaNac > DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException("La fecha de nacimiento del futbolista es inválida.", nameof(futbolista));
        }

        if (futbolista.cotizacion < 0)
        {
            throw new ArgumentException("La cotización del futbolista no puede ser negativa.", nameof(futbolista));
        }

        if (futbolista.puntuaciones is null || futbolista.puntuaciones.Count == 0)
        {
            throw new ArgumentException("El futbolista debe tener al menos una puntuación.", nameof(futbolista));
        }

        futbolista.nombre = futbolista.nombre.Trim();
        futbolista.apellido = futbolista.apellido.Trim();
        return _repository.Agregar(futbolista);
    }

    public bool Eliminar(ushort id)
    {
        return _repository.Eliminar(id);
    }
}
