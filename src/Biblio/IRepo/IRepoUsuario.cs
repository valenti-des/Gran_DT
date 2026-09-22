using Biblio;

namespace BiblioConDapper.IRepo;

public interface IRepoUsuario
{
    void AltaUsuario(Usuario usuario, string contraseña);

    Usuario? UsuarioPorContraseña(string email, string contraseña);
}
