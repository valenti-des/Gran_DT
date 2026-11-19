namespace GranDT.Dominio;

public sealed class RecursoNoEncontradoException(string message) : Exception(message);