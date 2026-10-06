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
        repo.Lista.Add(new Futbolista
        {
            idFutbolista = 1,
            nombre = "A",
            puntuaciones = [new Puntuacion { idPuntuacion = 4, puntuacion = 8.5f }]
        });
        var svc = new ServiciosFutbolista(repo);
        var res = svc.ObtenerTodos();

        Assert.Single(res);
        Assert.Equal("A", res[0].Nombre);
        Assert.Equal(4u, Assert.Single(res[0].Puntuaciones).IdPuntuacion);
    }

    [Fact]
    public void Agregar_RetornaFutbolistaAgregado()
    {
        var repo = new FakeRepo();
        var svc = new ServiciosFutbolista(repo);
        var request = new CrearFutbolistaRequest(
            "B", "Apellido", "Apodo", new DateOnly(2000, 1, 2), 120m, 3, 4);

        var res = svc.Agregar(request);

        var agregado = Assert.Single(repo.Lista);
        Assert.Equal("B", agregado.nombre);
        Assert.Equal("Apellido", agregado.apellido);
        Assert.Equal("Apodo", agregado.apodo);
        Assert.Equal(new DateOnly(2000, 1, 2), agregado.fechaNac);
        Assert.Equal(120m, agregado.cotizacion);
        Assert.Equal((byte)3, agregado.idEquipo);
        Assert.Equal((byte)4, agregado.idTipoFutbolista);
        Assert.Equal("B", res.Nombre);
    }

    [Fact]
    public void ObtenerPorNombre_SinCoincidencia_RetornaNull()
    {
        var svc = new ServiciosFutbolista(new FakeRepo());

        Assert.Null(svc.ObtenerPorNombre("Inexistente"));
    }

    [Fact]
    public void Eliminar_RetornaTrueCuandoExiste()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Futbolista { idFutbolista = 3, nombre = "C" });
        var svc = new ServiciosFutbolista(repo);
        var ok = svc.Eliminar("C");
        Assert.True(ok);
        Assert.Empty(repo.Lista);
    }
}