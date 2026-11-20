using Biblio;
using System.Text.Json;

namespace TestUnitarios;

[Trait("Category", "Unit")]
public class TestUnidadPuraPlantillaFutbolista
{
    [Fact]
    public void PlantillaFutbolistaCreacion_DeberiaInicializarYConservarSusPropiedades()
    {
        var detalle = new PlantillaFutbolistaCreacion();

        Assert.False(detalle.futbolistaTitular);
        Assert.Equal((uint)0, detalle.idPlantilla);
        Assert.Equal((ushort)0, detalle.idFutbolista);

        detalle.futbolistaTitular = true;
        detalle.idPlantilla = 2;
        detalle.idFutbolista = 3;

        Assert.True(detalle.futbolistaTitular);
        Assert.Equal((uint)2, detalle.idPlantilla);
        Assert.Equal((ushort)3, detalle.idFutbolista);

        var json = JsonSerializer.Serialize(detalle, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        using var documento = JsonDocument.Parse(json);
        var propiedades = documento.RootElement.EnumerateObject()
            .Select(propiedad => propiedad.Name)
            .OrderBy(nombre => nombre)
            .ToArray();

        Assert.Equal(new[] { "futbolistaTitular", "idFutbolista", "idPlantilla" }, propiedades);
    }
}
