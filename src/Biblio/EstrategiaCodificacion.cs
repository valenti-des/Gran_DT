using Biblio.IRepo;

namespace Biblio
{
    public class EstrategiaCodificacion : IEstrategiaCodificacion
    {
        public string codificar(string contrasena)
        {
            return BCrypt.Net.BCrypt.HashPassword(contrasena);
        }
    }
}