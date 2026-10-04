namespace Biblio.IRepo;

public interface IRepoUsuario
{
    List<Usuario> ObtenerTodos();
    Usuario? ObtenerPorNombre(string nombre);
    Usuario? ObtenerPorEmail(string email);
    Usuario Agregar(Usuario usuario);
    bool Eliminar(string email);
}

