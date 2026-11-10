using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosUsuario
{
    private readonly IRepoUsuario _repository;

    public ServiciosUsuario(IRepoUsuario repository)
    {
        _repository = repository;
    }

    public List<Usuario> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Usuario? ObtenerPorId(ushort idUsuario)
    {
        return _repository.ObtenerPorId(idUsuario);
    }

    public Usuario Agregar(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (string.IsNullOrWhiteSpace(usuario.contraseña))
        {
            throw new ArgumentException("La contraseña es obligatoria.", nameof(usuario));
        }

        usuario.contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.contraseña);
        return _repository.Agregar(usuario);
    }

    public bool Eliminar(ushort idUsuario)
    {
        return _repository.Eliminar(idUsuario);
    }
}