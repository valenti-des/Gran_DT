namespace Biblio.IRepo;

public interface IEquipoRepository
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorNombre(string nombre);
    Equipo Agregar(Equipo equipo);
    bool Eliminar(string nombre);
}