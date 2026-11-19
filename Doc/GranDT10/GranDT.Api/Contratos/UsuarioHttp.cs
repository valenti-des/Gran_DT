using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using GranDT.Aplicacion;

namespace GranDT.Api;

public sealed record CrearUsuarioHttp
{
    [Required] public string Nombre { get; init; } = "";
    [Required] public string Apellido { get; init; } = "";
    [Required, EmailAddress] public string Email { get; init; } = "";
    public DateOnly? FechaNac { get; init; }
    [Required, MinLength(8)] public string Contrasena { get; init; } = "";
    [JsonPropertyName("es_admin")] public bool EsAdmin { get; init; }

    public CrearUsuario AAplicacion() => new(Nombre, Apellido, Email, FechaNac, Contrasena, EsAdmin);
}

public sealed record UsuarioRespuesta(long IdUsuario, string Nombre, string Apellido, string Email,
    DateOnly? FechaNac, [property: JsonPropertyName("es_admin")] bool EsAdmin);