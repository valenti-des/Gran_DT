namespace Biblio;

public class Usuario
{
    public short idUsuario { get; set; }
    public string nombre { get; set; } = string.Empty;
    public string apellido { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public DateTime? fechaNac { get; set; }
    public string contraseña { get; set; } = string.Empty;
    public bool es_admin { get; set; }
}
