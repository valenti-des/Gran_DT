using System.Security.Cryptography;
using GranDT.Aplicacion.IRepo;

namespace GranDT.Infraestructura;

public sealed class HashContrasenas : IHashContrasenas
{
    private const int Iteraciones = 210_000;

    public string Crear(string contrasena)
    {
        var sal = RandomNumberGenerator.GetBytes(16);
        var derivado = Rfc2898DeriveBytes.Pbkdf2(contrasena, sal, Iteraciones, HashAlgorithmName.SHA256, 32);
        return $"pbkdf2-sha256${Iteraciones}${Convert.ToBase64String(sal)}${Convert.ToBase64String(derivado)}";
    }
}