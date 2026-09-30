namespace Biblio.IRepo;

public interface IRepoUsuario
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorEmail(string email);
    Usuario Agregar(Usuario usuario);
    bool Eliminar(string email);
}

