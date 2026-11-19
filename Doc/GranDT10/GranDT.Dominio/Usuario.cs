namespace GranDT.Dominio;

public sealed record Usuario(long IdUsuario, string Nombre, string Apellido, string Email, DateOnly? FechaNac, bool EsAdmin);