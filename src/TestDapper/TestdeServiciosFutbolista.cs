using System.Collections.Generic;
using System.Linq;
using Xunit;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace TestUnitarios;

public class TestdeServiciosFutbolista
{
    class FakeRepo : IRepoFutbolista
    {
        public List<Futbolista> Lista = new();
        public List<Futbolista> ObtenerTodos() => Lista;
        public Futbolista? ObtenerPorId(byte id) => Lista.FirstOrDefault(f => f.idFutbolista == id);
        public Futbolista? ObtenerPorNombre(string nombre) => Lista.FirstOrDefault(f => f.nombre == nombre);
        public Futbolista Agregar(Futbolista futbolista) { Lista.Add(futbolista); return futbolista; }
        public bool Eliminar(string nombre) {
            var e = ObtenerPorNombre(nombre);
            if (e == null) return false;
            Lista.Remove(e);
            return true;
        }
    }

    [Fact]
    public void ObtenerTodos_RetornaLista()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Futbolista { idFutbolista = 1, nombre = "A" });
        var svc = new ServiceFutbolista(repo);
        var res = svc.ObtenerTodos();
        Assert.Single(res);
        Assert.Equal("A", res[0].nombre);
    }

    [Fact]
    public void Agregar_RetornaFutbolistaAgregado()
    {
        var repo = new FakeRepo();
        var svc = new ServiceFutbolista(repo);
        var nuevo = new Futbolista { idFutbolista = 2, nombre = "B" };
        var res = svc.Agregar(nuevo);
        Assert.Contains(res, repo.Lista);
        Assert.Equal("B", res.nombre);
    }

    [Fact]
    public void Eliminar_RetornaTrueCuandoExiste()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Futbolista { idFutbolista = 3, nombre = "C" });
        var svc = new ServiceFutbolista(repo);
        var ok = svc.Eliminar("C");
        Assert.True(ok);
        Assert.Empty(repo.Lista);
    }
}