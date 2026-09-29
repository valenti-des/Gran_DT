using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblio;

namespace Biblio.IRepo
{
    public interface IRepoPlantillaFutbolista
    {
        int AltaPlantillaFutbolista(PlantillaFutbolista plantillaFutbolista);
        PlantillaFutbolista? ObtenerPlantillaFutbolistaPorId(int idPlantillaFutbolista);
    }
}