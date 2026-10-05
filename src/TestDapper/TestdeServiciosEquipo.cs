using System.Collections.Generic;
using System.Linq;
using Xunit;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace TestUnitarios;

public class TestdeServiciosEquipo
{
    class FakeRepo : IRepoEquipo
    {
        public List<Equipo> Lista = new();
        public List<Equipo> ObtenerTodos() => Lista;
        public Equipo? ObtenerPorId(byte id) => Lista.FirstOrDefault(e => e.idEquipo == id);
        public Equipo? ObtenerPorNombre(string nombre) => Lista.FirstOrDefault(e => e.nombre == nombre);
        public Equipo Agregar(Equipo equipo) { Lista.Add(equipo); return equipo; }
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
        repo.Lista.Add(new Equipo { idEquipo = 1, nombre = "A" });
        var svc = new ServiceEquipo(repo);
        var res = svc.ObtenerTodos();
        Assert.Single(res);
        Assert.Equal("A", res[0].nombre);
    }

    [Fact]
    public void Agregar_RetornaEquipoAgregado()
    {
        var repo = new FakeRepo();
        var svc = new ServiceEquipo(repo);
        var nuevo = new Equipo { idEquipo = 2, nombre = "B" };
        var res = svc.Agregar(nuevo);
        Assert.Contains(res, repo.Lista);
        Assert.Equal("B", res.nombre);
    }

    [Fact]
    public void Eliminar_RetornaTrueCuandoExiste()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Equipo { idEquipo = 3, nombre = "C" });
        var svc = new ServiceEquipo(repo);
        var ok = svc.Eliminar("C");
        Assert.True(ok);
        Assert.Empty(repo.Lista);
    }
}