using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios ;

public class ServiciosUsuario
{
    private readonly IRepoUsuario repository;

    public ServiciosUsuario(IRepoUsuario repository)
    {
        this.repository = repository;
    }

    public List<Usuario> ObtenerTodos()
    {
        return repository.ObtenerTodos();
    }

    public Usuario? ObtenerPorNombre(string nombre)
    {
        return repository.ObtenerPorNombre(nombre);
    }

    public Usuario Agregar(Usuario usuario)
    {
        return repository.Agregar(usuario);
    }

    public bool Eliminar(string nombre)
    {
        return repository.Eliminar(nombre);
    }
}