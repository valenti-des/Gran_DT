using GranDT.Dominio;

namespace GranDT.Pruebas;

internal static class DatosPrueba
{
    public static List<PlantillaFutbolista> Titulares(decimal cotizacion = 5_000_000m)
    {
        var plantilla = new List<PlantillaFutbolista>();
        var id = 1L;
        Agregar(TipoFutbolista.Arquero, 1);
        Agregar(TipoFutbolista.Defensor, 4);
        Agregar(TipoFutbolista.Mediocampista, 4);
        Agregar(TipoFutbolista.Delantero, 2);
        return plantilla;

        void Agregar(TipoFutbolista tipo, int cantidad)
        {
            for (var i = 0; i < cantidad; i++)
            {
                var futbolista = new Futbolista(id, "Nombre", "Apellido", null, null, cotizacion, 1, tipo, []);
                plantilla.Add(new PlantillaFutbolista(1, futbolista, true));
                id++;
            }
        }
    }
}
