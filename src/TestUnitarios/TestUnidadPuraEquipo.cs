using Biblio;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraEquipo
{
    [Fact]
    public void Equipo_DeberiaInicializarYConservarSusPropiedades()
    {
        var equipo = new Equipo();

        Assert.Equal((byte)0, equipo.idEquipo);
        Assert.Equal(string.Empty, equipo.nombre);

        equipo.idEquipo = 2;
        equipo.nombre = "Boca Juniors";

        Assert.Equal((byte)2, equipo.idEquipo);
        Assert.Equal("Boca Juniors", equipo.nombre);
    }

    [Fact]
    public void EquipoCreacion_DeberiaInicializarYConservarElNombre()
    {
        var equipo = new EquipoCreacion();

        Assert.Equal(string.Empty, equipo.nombre);

        equipo.nombre = "River Plate";

        Assert.Equal("River Plate", equipo.nombre);
    }
}
