using System.Collections.Generic;
using System.Linq;
using Xunit;
using Biblio;
using Biblio.IRepo;
using Servicios;

namespace TestUnitarios;

public class TestdeServiciosPlantilla
{
    class FakeRepo : IRepoPlantilla
    {
        public List<Plantilla> Lista = new();

        public List<Plantilla> ObtenerTodos() => Lista;
        public Plantilla? ObtenerPorNombre(string nombre) => Lista.FirstOrDefault(p => p.nombreP == nombre);
        public Plantilla Agregar(Plantilla plantilla) { Lista.Add(plantilla); return plantilla; }
        public bool Eliminar(string nombre)
        {
            var p = ObtenerPorNombre(nombre);
            if (p == null) return false;
            Lista.Remove(p);
            return true;
        }
    }

    [Fact]
    public void ObtenerTodos_RetornaLista()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Plantilla { idPlantilla = 1, nombreP = "Primera" });

        var svc = new ServiciosPlantilla(repo);
        var res = svc.ObtenerTodos();

        Assert.Single(res);
        Assert.Equal("Primera", res[0].nombreP);
    }

    [Fact]
    public void Agregar_RetornaPlantillaAgregada()
    {
        var repo = new FakeRepo();
        var svc = new ServiciosPlantilla(repo);
        var nueva = new Plantilla { idPlantilla = 2, nombreP = "Segunda" };

        var res = svc.Agregar(nueva);

        Assert.Contains(res, repo.Lista);
        Assert.Equal("Segunda", res.nombreP);
    }

    [Fact]
    public void Agregar_RechazaJugadoresDuplicadosPorNombreApellidoYAnioNacimiento()
    {
        var repo = new FakeRepo();
        var svc = new ServiciosPlantilla(repo);
        var futbolista1 = new Futbolista
        {
            nombre = "Ana",
            apellido = "Perez",
            fechaNac = new DateOnly(2000, 1, 10)
        };
        var futbolista2 = new Futbolista
        {
            nombre = "Ana",
            apellido = "Perez",
            fechaNac = new DateOnly(2000, 11, 20)
        };
        var plantilla = new Plantilla
        {
            detalles =
            {
                new PlantillaFutbolista { futbolista = futbolista1 },
                new PlantillaFutbolista { futbolista = futbolista2 }
            }
        };

        var excepcion = Assert.Throws<ArgumentException>(() => svc.Agregar(plantilla));

        Assert.Equal("No se puede agregar la plantilla: contiene jugadores duplicados.", excepcion.Message);
        Assert.Empty(repo.Lista);
    }

    [Fact]
    public void Eliminar_RetornaTrueCuandoExiste()
    {
        var repo = new FakeRepo();
        repo.Lista.Add(new Plantilla { idPlantilla = 3, nombreP = "Tercera" });

        var svc = new ServiciosPlantilla(repo);
        var ok = svc.Eliminar("Tercera");

        Assert.True(ok);
        Assert.Empty(repo.Lista);
    }
}