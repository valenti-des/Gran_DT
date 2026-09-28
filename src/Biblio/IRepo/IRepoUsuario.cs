using Biblio;

namespace BiblioConDapper.IRepo;

public interface IRepoUsuario
{

    int AltaUsuario(Usuario usuario);
    Usuario? LoginUsuario(string email, string contrasena);

}
