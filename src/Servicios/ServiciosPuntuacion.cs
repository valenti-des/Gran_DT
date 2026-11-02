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

    public Puntuacion? ObtenerPorId(int id)
    {
        return _repository.ObtenerPorId(id.ToString());
    }

    public List<Puntuacion> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Puntuacion Agregar(Puntuacion puntuacion)
    {

        if (puntuacion.puntuacion < 1.0 || puntuacion.puntuacion > 10.0)
        {
            throw new ArgumentException("La puntuación debe estar entre 1.0 y 10.0.", nameof(puntuacion));
        }

        if (puntuacion.numFech < 1 || puntuacion.numFech > 50)
        {
            throw new ArgumentException("El número de fecha debe estar entre 1 y 50.", nameof(puntuacion));
        }
        var Resultado = _repository.ObtenerTodos(puntuacion);

        return _repository.Agregar(puntuacion);
    }

    public bool Eliminar(int id)
    {
        return _repository.Eliminar(id.ToString());
    }

}
