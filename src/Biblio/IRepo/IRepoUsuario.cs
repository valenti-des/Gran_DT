namespace Biblio.IRepo;

public interface IRepoUsuario
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorId(ushort id);
    Usuario? ObtenerPorEmail(string email);
    Usuario Agregar(Usuario usuario);
    bool Eliminar(ushort id);
}

