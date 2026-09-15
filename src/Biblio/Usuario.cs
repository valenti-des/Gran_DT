namespace Biblio;

public class Usuario
{
    public int id_usuario { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateTime fecha_nacimiento { get; set; }
    public string password { get; set; } = string.Empty; // debe ser de 64 caracteres
    public int id_rol { get; set; } // FK -> Rol
    public bool Administracion {get; set;}
}
