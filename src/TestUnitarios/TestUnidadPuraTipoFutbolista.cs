using Biblio;

namespace TestUnitarios
{
    public class TestUnidadPuraTipoFutbolista
    {
        [Fact]
        public void TipoFutbolista_DeberiaInicializarYConservarSusPropiedades()
        {
            var tipoFutbolista = new TipoFutbolista();

            Assert.Equal(0, tipoFutbolista.idTipoFutbolista);
            Assert.Equal(string.Empty, tipoFutbolista.tipoFutbolista);

            tipoFutbolista.idTipoFutbolista = 3;
            tipoFutbolista.tipoFutbolista = "Delantero";

            Assert.Equal(3, tipoFutbolista.idTipoFutbolista);
            Assert.Equal("Delantero", tipoFutbolista.tipoFutbolista);
        }

        [Fact]
        public void TipoFutbolistaCreacion_DeberiaInicializarYConservarElTipo()
        {
            var tipoFutbolista = new TipoFutbolistaCreacion();

            Assert.Equal(string.Empty, tipoFutbolista.tipoFutbolista);

            
             tipoFutbolista.idTipoFutbolista = 1;
            tipoFutbolista.tipoFutbolista = "Arquero";

            Assert.Equal("Arquero", tipoFutbolista.tipoFutbolista);
        }
    }
}