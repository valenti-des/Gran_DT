using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Biblio;

namespace Biblio.IRepo
{
    public interface IRepoPlantilla
    {
        int AltaPlantilla(Plantilla plantilla);
        Plantilla? AltaPlantillaPorId(int idPlantilla);
    }
}