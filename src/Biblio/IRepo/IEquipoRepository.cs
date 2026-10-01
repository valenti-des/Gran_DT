namespace Biblio.IRepo;

public interface IEquipoRepository
{
    List<Equipo> ObtenerTodos();
    Equipo? ObtenerPorId(byte id);
    Equipo Agregar(Equipo equipo);
    bool Eliminar(byte id);
}