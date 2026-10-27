namespace Biblio.IRepo;

public interface IRepoEquipo
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorId(byte idEquipo);
    Equipo Agregar(Equipo equipo);
    bool Eliminar(byte idEquipo);
}