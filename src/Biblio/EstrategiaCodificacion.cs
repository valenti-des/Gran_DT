using Biblio.IRepo;

namespace Biblio
{
    public class EstrategiaCodificacion : IEstrategiaCodificacion
    {
        public string codificar(string contraseña)
        {
            return BCrypt.Net.BCrypt.HashPassword(contraseña);
        }
    }
}