using System.Collections.Generic;
using System.Linq;
using Xunit;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace TestUnitarios;

public class TestdeServiciosUsuario
{
    class FakeRepo : IRepoUsuario
    {
        public List<Usuario> Lista = new();

        public List<Usuario> ObtenerTodos() => Lista;
        public Usuario? ObtenerPorNombre(string nombre) => Lista.FirstOrDefault(u => u.nombre == nombre);
        public Usuario? ObtenerPorEmail(string email) => Lista.FirstOrDefault(u => u.email == email);
        public Usuario Agregar(Usuario usuario) { Lista.Add(usuario); return usuario; }
        public bool Eliminar(string email)
        {
            var usuario = ObtenerPorEmail(email);
            if (usuario == null) return false;
            Lista.Remove(usuario);
            return true;
        }
    }

    [Fact]
    public void ObtenerTodos_RetornaLista()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Usuario { idUsuario = 1, nombre = "usuario1", email = "u1@test.com", contraseña = "1234" });

        var svc = new ServiciosUsuario(repo);
        var res = svc.ObtenerTodos();

        Assert.Single(res);
        Assert.Equal("usuario1", res[0].nombre);
    }

    [Fact]
    public void Agregar_RetornaUsuarioAgregado()
    {
        var repo = new FakeRepo();
        var svc = new ServiciosUsuario(repo);
        var nuevo = new Usuario { idUsuario = 2, nombre = "usuario2", email = "u2@test.com", contraseña = "clave" };

        var res = svc.Agregar(nuevo);

        Assert.Contains(res, repo.Lista);
        Assert.Equal("usuario2", res.nombre);
        Assert.NotEqual("clave", res.contraseña);
    }

    [Fact]
    public void Eliminar_RetornaTrueCuandoExiste()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Usuario { idUsuario = 3, nombre = "usuario3", email = "u3@test.com", contraseña = "1234" });

        var svc = new ServiciosUsuario(repo);
        var ok = svc.Eliminar("u3@test.com");

        Assert.True(ok);
        Assert.Empty(repo.Lista);
    }
}