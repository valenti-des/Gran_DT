using System;

namespace Biblio;

public class Usuario
{
    public short idUsuario { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateOnly? fechaNac { get; set; } // Cambiado a DateOnly
    public string contraseña { get; set; } = string.Empty;
    public bool es_admin { get; set; }

    // Método para verificar si el usuario es administrador
    public bool EsAdministrador()
    {
        return es_admin;
    }
}