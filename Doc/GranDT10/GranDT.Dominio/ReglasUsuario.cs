using System.Text.RegularExpressions;

namespace GranDT.Dominio;

public static partial class ReglasUsuario
{
    public static void Validar(string nombre, string apellido, string email, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apellido))
            throw new ReglaNegocioException("Nombre y apellido son obligatorios.");
        if (string.IsNullOrWhiteSpace(email) || !EmailValido().IsMatch(email))
            throw new ReglaNegocioException("El correo electrónico no es válido.");
        if (string.IsNullOrWhiteSpace(contrasena) || contrasena.Length < 8)
            throw new ReglaNegocioException("La contraseña debe tener al menos 8 caracteres.");
    }

    [GeneratedRegex("^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$")]
    private static partial Regex EmailValido();
}