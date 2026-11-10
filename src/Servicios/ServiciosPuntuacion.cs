using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosPuntuacion
{
    private readonly IRepoPuntuacion _repository;

    public ServiciosPuntuacion(IRepoPuntuacion repository)
    {
        _repository = repository;
    }

    public Puntuacion? ObtenerPorId(uint id)
    {
        return _repository.ObtenerPorId(id);
    }

    public List<Puntuacion> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Puntuacion Agregar(Puntuacion puntuacion)
    {
        ArgumentNullException.ThrowIfNull(puntuacion);

        if (puntuacion.puntuacion is null or < 1m or > 10m)
        {
            throw new ArgumentException("La puntuación debe estar entre 1.0 y 10.0.", nameof(puntuacion));
        }

        if (puntuacion.numFech is null or < 1 or > 50)
        {
            throw new ArgumentException("El número de fecha debe estar entre 1 y 50.", nameof(puntuacion));
        }

        return _repository.Agregar(puntuacion);
    }

    public bool Eliminar(uint id)
    {
        return _repository.Eliminar(id);
    }
}
