using GranDT.Dominio;

namespace GranDT.Api;

public static class MapeoHttp
{
    public static EquipoRespuesta AHttp(this Equipo equipo) => new(equipo.IdEquipo, equipo.Nombre);
    public static PuntuacionRespuesta AHttp(this Puntuacion puntuacion) =>
        new(puntuacion.IdPuntuacion, puntuacion.IdFutbolista, puntuacion.Valor, puntuacion.NroFecha);
    public static FutbolistaRespuesta AHttp(this Futbolista futbolista) => new(
        futbolista.IdFutbolista, futbolista.Nombre, futbolista.Apellido, futbolista.Apodo,
        futbolista.FechaNac, futbolista.Cotizacion, futbolista.IdEquipo, (int)futbolista.Tipo,
        futbolista.Puntuaciones.Select(AHttp).ToArray());
    public static UsuarioRespuesta AHttp(this Usuario usuario) => new(
        usuario.IdUsuario, usuario.Nombre, usuario.Apellido, usuario.Email, usuario.FechaNac, usuario.EsAdmin);
    public static PlantillaRespuesta AHttp(this Plantilla plantilla) => new(
        plantilla.IdPlantilla, plantilla.NombreP, plantilla.Presupuesto, plantilla.CantFutbolista,
        plantilla.IdUsuario, plantilla.Detalles.Select(d => new PlantillaFutbolistaRespuesta(
            d.IdPlantilla, d.Futbolista.IdFutbolista, d.FutbolistaTitular)).ToArray());
}