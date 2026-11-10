using Biblio;
using Biblio.IRepo;
using Moq;
using Servicios;

namespace GranDTApi.Tests.Services;

public class ServiciosUsuarioTests
{
    private readonly Mock<IRepoUsuario> _repoUsuarioMock = new();
    private readonly ServiciosUsuario _serviciosUsuario;

    public ServiciosUsuarioTests()
    {
        _serviciosUsuario = new ServiciosUsuario(_repoUsuarioMock.Object);
    }

    [Fact]
    public void Agregar_UsuarioValido_HasheaLaContraseñaYDevuelveUsuarioCreado()
    {
        const string contraseñaOriginal = "ClaveSegura123!";
        var usuario = new Usuario
        {
            nombre = "Ana",
            apellido = "Pérez",
            email = "ana@example.com",
            contraseña = contraseñaOriginal
        };
        _repoUsuarioMock
            .Setup(repo => repo.Agregar(It.Is<Usuario>(u => BCrypt.Net.BCrypt.Verify(contraseñaOriginal, u.contraseña))))
            .Returns((Usuario u) => u);

        var resultado = _serviciosUsuario.Agregar(usuario);

        Assert.Same(usuario, resultado);
        Assert.NotEqual(contraseñaOriginal, resultado.contraseña);
        Assert.True(BCrypt.Net.BCrypt.Verify(contraseñaOriginal, resultado.contraseña));
        _repoUsuarioMock.Verify(
            repo => repo.Agregar(It.Is<Usuario>(u => BCrypt.Net.BCrypt.Verify(contraseñaOriginal, u.contraseña))),
            Times.Once);
    }

    [Fact]
    public void Agregar_ContraseñaVacia_LanzaArgumentExceptionSinLlamarAlRepositorio()
    {
        var usuario = new Usuario { contraseña = " " };

        var excepcion = Assert.Throws<ArgumentException>(() => _serviciosUsuario.Agregar(usuario));

        Assert.StartsWith("La contraseña es obligatoria.", excepcion.Message);
        _repoUsuarioMock.Verify(repo => repo.Agregar(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public void ObtenerTodos_ExistenUsuarios_DevuelveColeccionCompleta()
    {
        var usuarios = new List<Usuario>
        {
            new() { idUsuario = 1, 
                    nombre = "Ana", 
                    apellido = "Pérez" },
            new() { idUsuario = 2, 
                    nombre = "Luis", 
                    apellido = "Gómez" }
        };
        _repoUsuarioMock.Setup(repo => repo.ObtenerTodos()).Returns(usuarios);

        var resultado = _serviciosUsuario.ObtenerTodos();

        Assert.Equal(usuarios, resultado);
        _repoUsuarioMock.Verify(repo => repo.ObtenerTodos(), Times.Once);
    }

    [Fact]
    public void ObtenerPorId_UsuarioExiste_DevuelveUsuario()
    {
        const ushort idBuscado = 7;
        var usuario = new Usuario { idUsuario = idBuscado, nombre = "Ana", apellido = "Pérez" };
        _repoUsuarioMock.Setup(repo => repo.ObtenerPorId(idBuscado)).Returns(usuario);

        var resultado = _serviciosUsuario.ObtenerPorId(idBuscado);

        Assert.Same(usuario, resultado);
        _repoUsuarioMock.Verify(repo => repo.ObtenerPorId(idBuscado), Times.Once);
    }

    [Fact]
    public void Eliminar_IdValido_DevuelveResultadoDelRepositorio()
    {
        const ushort idAEliminar = 15;
        _repoUsuarioMock.Setup(repo => repo.Eliminar(idAEliminar)).Returns(true);

        var resultado = _serviciosUsuario.Eliminar(idAEliminar);

        Assert.True(resultado);
        _repoUsuarioMock.Verify(repo => repo.Eliminar(idAEliminar), Times.Once);
    }
}
