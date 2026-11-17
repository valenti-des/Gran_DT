using System.Collections.Generic;
using Biblio;
using Biblio.IRepo;

namespace Servicios;

public class ServiciosUsuario
{
    private readonly IRepoUsuario _repository;
    private readonly IEstrategiaCodificacion _estrategiaCodificacion;

    public ServiciosUsuario(
        IRepoUsuario repository,
        IEstrategiaCodificacion estrategiaCodificacion)
    {
        _repository = repository;
        _estrategiaCodificacion = estrategiaCodificacion;
    }

    public List<Usuario> ObtenerTodos()
    {
        return _repository.ObtenerTodos();
    }

    public Usuario? obtenerPorEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("El email es obligatorio.", nameof(email));
        }

        return _repository.ObtenerPorEmail(email);
    }

    public Usuario? ObtenerPorId(ushort idUsuario)
    {
        return _repository.ObtenerPorId(idUsuario);
    }

    public Usuario Agregar(Usuario usuario)
    {
        ArgumentNullException.ThrowIfNull(usuario);

        if (string.IsNullOrWhiteSpace(usuario.contrasena))
        {
            throw new ArgumentException("La contraseña es obligatoria.", nameof(usuario));
        }

        usuario.contrasena = _estrategiaCodificacion.codificar(usuario.contrasena);
        return _repository.Agregar(usuario);
    }

    public bool Eliminar(ushort idUsuario)
    {
        return _repository.Eliminar(idUsuario);
    }

    

}