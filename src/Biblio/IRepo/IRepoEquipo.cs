namespace Biblio.IRepo;

public interface IRepoEquipo
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorId(byte id);
    Equipo Agregar(Equipo equipo);
    bool Eliminar(string nombre);
}