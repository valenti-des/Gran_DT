namespace GranDT.Aplicacion;

public sealed record CrearFutbolista(string Nombre, string Apellido, string? Apodo, DateOnly? FechaNac,
    decimal Cotizacion, long IdEquipo, GranDT.Dominio.TipoFutbolista Tipo, IReadOnlyList<NuevaPuntuacion>? Puntuaciones);
public sealed record ActualizarFutbolista(string Nombre, string Apellido, string? Apodo, DateOnly? FechaNac,
    decimal Cotizacion, long IdEquipo, GranDT.Dominio.TipoFutbolista Tipo);
public sealed record NuevaPuntuacion(decimal Valor, int NroFecha);
public sealed record CrearUsuario(string Nombre, string Apellido, string Email, DateOnly? FechaNac, string Contrasena, bool EsAdmin);
public sealed record CrearPlantilla(string NombreP, decimal Presupuesto, int CantFutbolista);