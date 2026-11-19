namespace GranDT.Dominio;

public sealed class ConflictoException(string message) : Exception(message);